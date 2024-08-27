using Microsoft.Win32;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Xceed.Words.NET;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using DocumentFormat.OpenXml.Spreadsheet;
using PdfSharp.Drawing.Layout;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Controls.Primitives;
using DocumentFormat.OpenXml.Presentation;
using TextElement = System.Windows.Documents.TextElement;
using Xceed.Document.NET;
using List = Xceed.Document.NET.List;
using System.Text;


namespace ResumeBuilder
{

    public partial class MainWindow : Window
    {
        private string _photoPath;
        private XBrush headerBrush;
        private int yPoint;
       // private UIElement expanderExperienceContainer;
        private Dictionary<Button, string> deletedSkills = new Dictionary<Button, string>();
        public MainWindow()
        {
            InitializeComponent();
            rtbProfessionalSummary.TextChanged += RtbProfessionalSummary_TextChanged;
            rtbExperienceDescription.TextChanged += RtbExperienceDescription_TextChanged;
            rtbEducationDescription.TextChanged += RtbEducationDescription_TextChanged;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedIndex < tabControl.Items.Count - 1)
            {
                tabControl.SelectedIndex++;
            }
        }
        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedIndex > 0)
            {
                tabControl.SelectedIndex--;
            }
        }
        private void BrowsePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {

                _photoPath = openFileDialog.FileName;
                //imgPhoto.Source = new BitmapImage(new Uri(_photoPath));
                var bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                imgPhoto.Source = bitmap;

                // Hide the large icon button
                btnUploadPhoto.Visibility = Visibility.Collapsed;
                btnDeletePhoto.Visibility = Visibility.Visible;
            }
            
        }
        private void EditPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_photoPath))
            {
                BrowsePhoto_Click(sender, e);
            }
        }
        private void DeletePhoto_Click(object sender, RoutedEventArgs e)
        {
            // Display confirmation dialog
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete your photo?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            // Check the result of the dialog
            if (result == MessageBoxResult.Yes)
            {
                imgPhoto.Source = null; // Or your photo deletion logic here
                btnUploadPhoto.Visibility = Visibility.Visible;
                btnDeletePhoto.Visibility = Visibility.Collapsed;
            }
        }
        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            ToggleTextFormatting(TextElement.FontWeightProperty, FontWeights.Bold, FontWeights.Normal);
        }

        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            ToggleTextFormatting(TextElement.FontStyleProperty, FontStyles.Italic, FontStyles.Normal);
        }

        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            ToggleTextDecoration(TextDecorations.Underline);
        }

        private void Strikethrough_Click(object sender, RoutedEventArgs e)
        {
            ToggleTextDecoration(TextDecorations.Strikethrough);
        }

        private void Numbering_Click(object sender, RoutedEventArgs e)
        {
            ApplyListStyle(ListStyle.Numbering);
        }

        private void Bullets_Click(object sender, RoutedEventArgs e)
        {
            ApplyListStyle(ListStyle.Bullets);
        }

        // Helper method for toolbar function(Bold, Italic)
        private void ToggleTextFormatting(DependencyProperty formattingProperty, object applyValue, object removeValue)
        {
            RichTextBox richTextBox = GetFocusedRichTextBox();
            if (richTextBox != null)
            {
                TextSelection selection = richTextBox.Selection;
                if (selection.GetPropertyValue(formattingProperty).Equals(applyValue))
                {
                    selection.ApplyPropertyValue(formattingProperty, removeValue);
                }
                else
                {
                    selection.ApplyPropertyValue(formattingProperty, applyValue);
                }
                richTextBox.Focus();
            }
        }

        // Helper for toolbar function (Underline and Strikethrough)
        private void ToggleTextDecoration(TextDecorationCollection textDecoration)
        {
            RichTextBox richTextBox = GetFocusedRichTextBox();
            if (richTextBox != null)
            {
                TextSelection selection = richTextBox.Selection;
                TextDecorationCollection currentDecorations = (TextDecorationCollection)selection.GetPropertyValue(Inline.TextDecorationsProperty);
                if (currentDecorations == null || !currentDecorations.Contains(textDecoration[0]))
                {
                    currentDecorations = new TextDecorationCollection(currentDecorations);
                    currentDecorations.Add(textDecoration[0]);
                }
                else
                {
                    currentDecorations = new TextDecorationCollection(currentDecorations);
                    currentDecorations.Remove(textDecoration[0]);
                }
                selection.ApplyPropertyValue(Inline.TextDecorationsProperty, currentDecorations);
                richTextBox.Focus();
            }
        }

        private RichTextBox GetFocusedRichTextBox()
        {
            if (rtbProfessionalSummary.IsKeyboardFocusWithin)
                return rtbProfessionalSummary;
            if (rtbExperienceDescription.IsKeyboardFocusWithin)
                return rtbExperienceDescription;
            if (rtbEducationDescription.IsKeyboardFocusWithin)
                return rtbEducationDescription;
            return null;
        }

        // Helper method for toolbar funciton(Numbering and Bullets)
        private enum ListStyle
        {
            Numbering,
            Bullets
        }
        private void ApplyListStyle(ListStyle listStyle)
        {
            RichTextBox richTextBox = GetFocusedRichTextBox();
            if (richTextBox != null)
            {
                TextRange selectedTextRange = new TextRange(richTextBox.Selection.Start, richTextBox.Selection.End);

                if (selectedTextRange.Text.Length == 0)
                {
                    MessageBox.Show("Please select the text to apply the list style.");
                    return;
                }

                string[] lines = selectedTextRange.Text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                bool isNumbering = listStyle == ListStyle.Numbering;
                bool isBulleted = listStyle == ListStyle.Bullets;

                for (int i = 0; i < lines.Length; i++)
                {
                    if (isNumbering && !lines[i].StartsWith($"{i + 1}. "))
                    {
                        lines[i] = $"{i + 1}. " + lines[i];
                    }
                    else if (isBulleted && !lines[i].StartsWith("• "))
                    {
                        lines[i] = "• " + lines[i];
                    }
                    else if (isNumbering && lines[i].StartsWith($"{i + 1}. "))
                    {
                        lines[i] = lines[i].Substring(lines[i].IndexOf(' ') + 1);
                    }
                    else if (isBulleted && lines[i].StartsWith("• "))
                    {
                        lines[i] = lines[i].Substring(2);
                    }
                }

                selectedTextRange.Text = string.Join(Environment.NewLine, lines);
                richTextBox.Focus();
            }
        }

        // Function save as PDF file
        private void SaveAsPdf_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = "Resume.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                SaveAsPdf(filePath);
            }
        }
        private void SaveAsPdf(string fileName)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Resume";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Use default font style (regular)
            XFont font = new XFont("Verdana", 12); // Regular is the default style
            XFont titleFont = new XFont("Verdana", 14);
            XFont contentFont = new XFont("Verdana", 12);


            double yPoint = 20;

            double margin = 20;
            double pageWidth = page.Width - 2 * margin;

            // Add photo if exists
            if (imgPhoto.Source != null)
            {
                BitmapImage bitmapImage = imgPhoto.Source as BitmapImage;
                if (bitmapImage != null)
                {
                    MemoryStream stream = new MemoryStream();
                    PngBitmapEncoder encoder = new PngBitmapEncoder(); // Changed to PngBitmapEncoder
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    encoder.Save(stream);
                    stream.Seek(0, SeekOrigin.Begin);

                    XImage xImage = XImage.FromStream(stream);
                    double photoSize = 100;
                    double photoX = 20;
                    double photoY = yPoint;
                    XGraphicsPath path = new XGraphicsPath();
                    path.AddEllipse(photoX, photoY, photoSize, photoSize);
                    gfx.Save();
                    gfx.IntersectClip(path);
                    gfx.DrawImage(xImage, photoX, photoY, photoSize, photoSize);
                    gfx.Restore();
                    yPoint += photoSize + 10;
                }
            }
            // Personal Information
            gfx.DrawString("Personal Information", titleFont, XBrushes.DarkCyan, new XRect(20, yPoint, page.Width - 40, page.Height), XStringFormats.TopLeft);
            yPoint += 30;
            gfx.DrawString("Name: " + txtName.Text, contentFont, XBrushes.Black, new XRect(20, yPoint, page.Width - 40, page.Height), XStringFormats.TopLeft);
            yPoint += 20;
            gfx.DrawString("Email: " + txtEmail.Text, contentFont, XBrushes.Black, new XRect(20, yPoint, page.Width - 40, page.Height), XStringFormats.TopLeft);
            yPoint += 20;
            gfx.DrawString("Phone: " + txtPhone.Text, contentFont, XBrushes.Black, new XRect(20, yPoint, page.Width - 40, page.Height), XStringFormats.TopLeft);
            yPoint += 20;
            gfx.DrawString("Date of Birth: " + dpDateOfBirth.Text, contentFont, XBrushes.Black, new XRect(20, yPoint, page.Width - 40, page.Height), XStringFormats.TopLeft);
            yPoint += 20;
            gfx.DrawString("Wanted Job Title: " + txtWantedJobTitle.Text, contentFont, XBrushes.Black, new XRect(20, yPoint, page.Width - 40, page.Height), XStringFormats.TopLeft);
            yPoint += 30;

            // Professional Summary
            gfx.DrawString("Professional Summary", titleFont, XBrushes.DarkCyan, new XRect(20, yPoint, page.Width - 40, page.Height), XStringFormats.TopLeft);
            yPoint += 30;
            AddRichTextBoxContentToPdf(gfx, font, ref yPoint, "Description: ", rtbProfessionalSummary, XBrushes.Black);
            yPoint += 10;

            // Experience
            gfx.DrawString("Experience", titleFont, XBrushes.DarkCyan, new XRect(20, yPoint, page.Width - 40, page.Height), XStringFormats.TopLeft);
            yPoint += 30;
            // Iterate over remaining experience sections
            foreach (Grid expanderContainer in expanderExperience.Children)
            {
                if (expanderContainer.Children[0] is System.Windows.Controls.Border border && border.Child is Expander expander)
                {
                    if (expander.Content is StackPanel stackPanel)
                    {
                        // Extract and add experience details to the PDF
                        var txtCompany = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "txtCompany");
                        var txtRole = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "txtRole");
                        var txtCity = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "txtCity");
                        var dpStartDate = stackPanel.Children.OfType<DatePicker>().FirstOrDefault(dp => dp.Name == "dpStartDate");
                        var dpEndDate = stackPanel.Children.OfType<DatePicker>().FirstOrDefault(dp => dp.Name == "dpEndDate");
                        var rtbDescription = stackPanel.Children.OfType<Grid>().FirstOrDefault()?.Children.OfType<RichTextBox>().FirstOrDefault();

                        // Check if the expander still exists (i.e., not deleted)
                        if (txtCompany != null && txtRole != null && txtCity != null)
                        {
                            string experienceInfo = $"{txtRole.Text}, {txtCompany.Text.ToUpper()}, {txtCity.Text}";
                            XFont boldFont1 = new XFont("Verdana Bold", 12);
                            gfx.DrawString(experienceInfo, boldFont1, XBrushes.Black, new XRect(20, yPoint, page.Width - 40, page.Height), XStringFormats.TopLeft);
                            yPoint += 20;

                            string dateInfo = $"{dpStartDate.Text} - {dpEndDate.Text}";
                            gfx.DrawString(dateInfo, new XFont("Verdana Italic", 10), XBrushes.Gray, new XRect(20, yPoint, page.Width - 40, page.Height), XStringFormats.TopLeft);
                            yPoint += 20;

                            AddRichTextBoxContentToPdf(gfx, font, ref yPoint, "Description: ", rtbDescription, XBrushes.Black);
                            yPoint += 10;
                        }
                    }
                }
            }

            // Education section
            gfx.DrawString("Education", titleFont, XBrushes.DarkCyan, new XRect(20, yPoint, page.Width - 40, page.Height), XStringFormats.TopLeft);
            yPoint += 30;
            foreach (Grid expanderContainer in expanderEducation.Children)
            {
                if (expanderContainer.Children[0] is System.Windows.Controls.Border border && border.Child is Expander expander)
                {
                    if (expander.Content is StackPanel stackPanel)
                    {
                        // Extract and add education details to the PDF
                        var txtInstitution = stackPanel.Children.OfType<TextBox>().FirstOrDefault(x => x.Name == "txtInstitution");
                        var txtDegree = stackPanel.Children.OfType<TextBox>().FirstOrDefault(x => x.Name == "txtDegree");
                        var txtCity = stackPanel.Children.OfType<TextBox>().FirstOrDefault(x => x.Name == "txtCity");
                        var dpGraduationDate = stackPanel.Children.OfType<DatePicker>().FirstOrDefault(x => x.Name == "dpGraduationDate");
                        var rtbEducationDescription = stackPanel.Children.OfType<Grid>().FirstOrDefault()?.Children.OfType<RichTextBox>().FirstOrDefault();

                        if (txtDegree != null && txtInstitution != null && txtCity != null && dpGraduationDate != null && rtbEducationDescription != null)
                        {
                            string educationInfo = $"{txtDegree.Text}, {txtInstitution.Text.ToUpper()}, {txtCity.Text}";
                            XFont boldFont2 = new XFont("Verdana Bold", 12);
                            gfx.DrawString(educationInfo, boldFont2, XBrushes.Black, new XRect(20, yPoint, page.Width - 40, page.Height), XStringFormats.TopLeft);
                            yPoint += 20;

                            gfx.DrawString("Graduation Date: " + dpGraduationDate.Text, new XFont("Verdana Italic", 10), XBrushes.Gray, new XRect(20, yPoint, page.Width - 40, page.Height), XStringFormats.TopLeft);
                            yPoint += 20;

                            AddRichTextBoxContentToPdf(gfx, font, ref yPoint, "Description: ", rtbEducationDescription, XBrushes.Black);
                            yPoint += 10;
                        }
                    }
                }
            }


            // Skill Section
            gfx.DrawString("Skills", titleFont, XBrushes.DarkCyan, new XRect(20, yPoint, page.Width - 40, page.Height), XStringFormats.TopLeft);
            yPoint += 30;

            foreach (Grid expanderContainer in ExpanderList.Children)
            {
                if (expanderContainer.Children[0] is System.Windows.Controls.Border border && border.Child is Expander expander)
                {
                    if (expander.Header is StackPanel headerPanel)
                    {
                        // Extract the skill name
                        TextBlock skillNameTextBlock = headerPanel.Children[0] as TextBlock;
                        string skillName = skillNameTextBlock?.Text ?? "Unknown Skill";

                        // Extract the skill level
                        TextBlock skillLevelTextBlock = headerPanel.Children[1] as TextBlock;
                        string skillLevel = skillLevelTextBlock?.Text.Replace("Level: ", "") ?? "Unknown Level";

                        // Write skill name and level to the PDF
                        gfx.DrawString(skillName, contentFont, XBrushes.Black, new XRect(20, yPoint, pageWidth, page.Height), XStringFormats.TopLeft);
                        yPoint += 20;
                        XFont italicFont4 = new XFont("Verdana Italic", 10);
                        gfx.DrawString("Level: " + skillLevel, italicFont4, XBrushes.Gray, new XRect(20, yPoint, pageWidth, page.Height), XStringFormats.TopLeft);
                        yPoint += 20;
                    }
                }
            }

            document.Save(fileName);
            document.Close();
        }

        private void AddRichTextBoxContentToPdf(XGraphics gfx, XFont defaultFont, ref double yPoint, string label, RichTextBox richTextBox, XBrush brush)
        {
            // Define margins
            double margin = 20;
            double pageWidth = gfx.PageSize.Width - 2 * margin;

            // Add a label before the RichTextBox content
            gfx.DrawString(label, defaultFont, brush, new XRect(margin, yPoint, pageWidth, gfx.PageSize.Height), XStringFormats.TopLeft);
            yPoint += 20;

            // Extract the content of the RichTextBox
            TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

            // Create default font styles
            XFont regularFont = new XFont("Verdana", 12);
            XFont boldFont = new XFont("Verdana Bold", 12);
            XFont italicFont = new XFont("Verdana italic", 12);
            XFont boldItalicFont = new XFont("Verdana BoldItalic", 12);

            int listCounter = 1; // Used for numbering lists

            foreach (var block in richTextBox.Document.Blocks)
            {
                if (block is System.Windows.Documents.List list)
                {
                    foreach (var listItem in list.ListItems)
                    {
                        foreach (var paragraph in listItem.Blocks.OfType<System.Windows.Documents.Paragraph>())
                        {
                            foreach (var inline in paragraph.Inlines)
                            {
                                if (inline is System.Windows.Documents.Run run)
                                {
                                    // Check for styles
                                    bool isBold = run.FontWeight == FontWeights.Bold;
                                    bool isItalic = run.FontStyle == FontStyles.Italic;
                                    bool isUnderline = run.TextDecorations.Contains(TextDecorations.Underline[0]);
                                    bool isStrikethrough = run.TextDecorations.Contains(TextDecorations.Strikethrough[0]);

                                    // Select the appropriate font
                                    XFont currentFont = regularFont;
                                    if (isBold && isItalic) currentFont = boldItalicFont;
                                    else if (isBold) currentFont = boldFont;
                                    else if (isItalic) currentFont = italicFont;

                                    // Handle numbering and bullets
                                    string bulletOrNumber = list.MarkerStyle == TextMarkerStyle.Disc ? "•" : $"{listCounter}.";
                                    gfx.DrawString(bulletOrNumber, regularFont, brush, new XRect(margin, yPoint, pageWidth, gfx.PageSize.Height), XStringFormats.TopLeft);

                                    // Adjust margin for list content
                                    double listContentMargin = margin + 20;

                                    // Measure the text width
                                    string text = run.Text;
                                    double textWidth = gfx.MeasureString(text, currentFont).Width;

                                    // Check if text needs to be wrapped
                                    if (textWidth > pageWidth - listContentMargin)
                                    {
                                        // Split the text into multiple lines
                                        List<string> lines = SplitTextToFitWidth(gfx, text, currentFont, pageWidth - listContentMargin);

                                        // Draw each line
                                        foreach (string line in lines)
                                        {
                                            gfx.DrawString(line, currentFont, brush, new XRect(listContentMargin, yPoint, pageWidth - listContentMargin, gfx.PageSize.Height), XStringFormats.TopLeft);
                                            yPoint += 20;

                                            // Handle underline and strikethrough for wrapped lines
                                            if (isUnderline || isStrikethrough)
                                            {
                                                double lineStartX = listContentMargin;
                                                double lineEndX = listContentMargin + gfx.MeasureString(line, currentFont).Width;

                                                if (isUnderline)
                                                {
                                                    double underlineY = yPoint - 2;
                                                    gfx.DrawLine(XPens.Black, lineStartX, underlineY, lineEndX, underlineY);
                                                }
                                                if (isStrikethrough)
                                                {
                                                    double strikethroughY = yPoint - 10;
                                                    gfx.DrawLine(XPens.Black, lineStartX, strikethroughY, lineEndX, strikethroughY);
                                                }

                                            }
                                        }
                                    }
                                    else
                                    {
                                        // Draw the text without wrapping
                                        gfx.DrawString(text, currentFont, brush, new XRect(listContentMargin, yPoint, pageWidth - listContentMargin, gfx.PageSize.Height), XStringFormats.TopLeft);
                                        yPoint += 20;

                                        // Handle underline and strikethrough for single lines
                                        if (isUnderline || isStrikethrough)
                                        {
                                            double lineStartX = listContentMargin;
                                            double lineEndX = listContentMargin + textWidth;

                                            if (isUnderline)
                                            {
                                                double underlineY = yPoint - 2;
                                                gfx.DrawLine(XPens.Black, lineStartX, underlineY, lineEndX, underlineY);
                                            }
                                            if (isStrikethrough)
                                            {
                                                double strikethroughY = yPoint - 10;
                                                gfx.DrawLine(XPens.Black, lineStartX, strikethroughY, lineEndX, strikethroughY);
                                            }
                                        }
                                    }

                                    listCounter++;
                                }
                            }
                        }
                    }
                }
                else if (block is System.Windows.Documents.Paragraph paragraph)
                {
                    // Paragraphs not inside a List
                    foreach (var inline in paragraph.Inlines)
                    {
                        if (inline is System.Windows.Documents.Run run)
                        {
                            // Check for various styles
                            bool isBold = run.FontWeight == FontWeights.Bold;
                            bool isItalic = run.FontStyle == FontStyles.Italic;
                            bool isUnderline = run.TextDecorations.Contains(TextDecorations.Underline[0]);
                            bool isStrikethrough = run.TextDecorations.Contains(TextDecorations.Strikethrough[0]);

                            // Select the appropriate font
                            XFont currentFont = regularFont;
                            if (isBold && isItalic) currentFont = boldItalicFont;
                            else if (isBold) currentFont = boldFont;
                            else if (isItalic) currentFont = italicFont;

                            // Measure the text width
                            string text = run.Text;
                            double textWidth = gfx.MeasureString(text, currentFont).Width;

                            // Check if text needs to be wrapped
                            if (textWidth > pageWidth)
                            {
                                // Split the text into multiple lines
                                List<string> lines = SplitTextToFitWidth(gfx, text, currentFont, pageWidth);

                                // Draw each line
                                foreach (string line in lines)
                                {
                                    gfx.DrawString(line, currentFont, brush, new XRect(margin, yPoint, pageWidth, gfx.PageSize.Height), XStringFormats.TopLeft);
                                    yPoint += 20;

                                    // Handle underline and strikethrough for wrapped lines
                                    if (isUnderline || isStrikethrough)
                                    {
                                        double lineStartX = margin;
                                        double lineEndX = margin + gfx.MeasureString(line, currentFont).Width;

                                        if (isUnderline)
                                        {
                                            double underlineY = yPoint - 2;
                                            gfx.DrawLine(XPens.Black, lineStartX, underlineY, lineEndX, underlineY);
                                        }
                                        if (isStrikethrough)
                                        {
                                            double strikethroughY = yPoint - 10;
                                            gfx.DrawLine(XPens.Black, lineStartX, strikethroughY, lineEndX, strikethroughY);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // Draw the text without wrapping
                                gfx.DrawString(text, currentFont, brush, new XRect(margin, yPoint, pageWidth, gfx.PageSize.Height), XStringFormats.TopLeft);
                                yPoint += 20;

                                // Handle underline and strikethrough for single lines
                                if (isUnderline || isStrikethrough)
                                {
                                    double lineStartX = margin;
                                    double lineEndX = margin + textWidth;

                                    if (isUnderline)
                                    {
                                        double underlineY = yPoint - 2;
                                        gfx.DrawLine(XPens.Black, lineStartX, underlineY, lineEndX, underlineY);
                                    }
                                    if (isStrikethrough)
                                    {
                                        double strikethroughY = yPoint - 10;
                                        gfx.DrawLine(XPens.Black, lineStartX, strikethroughY, lineEndX, strikethroughY);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        // Function to split text into lines that fit within the page width
        private List<string> SplitTextToFitWidth(XGraphics gfx, string text, XFont font, double maxWidth)
        {
            List<string> lines = new List<string>();
            string[] words = text.Split(' ');
            string currentLine = "";

            foreach (string word in words)
            {
                string testLine = currentLine.Length == 0 ? word : currentLine + " " + word;
                double testWidth = gfx.MeasureString(testLine, font).Width;

                if (testWidth < maxWidth)
                {
                    currentLine = testLine;
                }
                else
                {
                    lines.Add(currentLine);
                    currentLine = word;
                }
            }

            if (currentLine.Length > 0)
            {
                lines.Add(currentLine);
            }

            return lines;
        }

        // Funciton save as Document file
        private void SaveAsDocx_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Word Documents (*.docx)|*.docx",
                FileName = "Resume.docx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                SaveAsDocx(filePath);
            }
        }

        private void SaveAsDoc_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Word Documents (*.doc)|*.doc",
                FileName = "Resume.doc"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                SaveAsDoc(filePath);
            }
        }
        private void SaveAsDoc(string filePath)
        {

            string docxFilePath = Path.ChangeExtension(filePath, ".docx");
            SaveAsDocx(docxFilePath);

            File.Copy(docxFilePath, filePath, true);
        }
        private void SaveAsDocx(string filePath)
        {
            using (DocX document = DocX.Create(filePath))
            {
                // Add photo if exists
                if (imgPhoto.Source != null)
                {
                    BitmapImage bitmapImage = imgPhoto.Source as BitmapImage;
                    if (bitmapImage != null)
                    {
                        // Convert BitmapImage to Bitmap
                        MemoryStream bitmapStream = new MemoryStream();
                        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                        encoder.Save(bitmapStream);

                        using (var originalBitmap = new System.Drawing.Bitmap(bitmapStream))
                        {
                            int diameter = Math.Min(originalBitmap.Width, originalBitmap.Height);
                            var circularImage = new System.Drawing.Bitmap(diameter, diameter);

                            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(circularImage))
                            {
                                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, diameter, diameter);
                                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                                path.AddEllipse(rect);
                                graphics.SetClip(path);
                                graphics.DrawImage(originalBitmap, 0, 0, diameter, diameter);
                            }

                            // Save circular image to memory stream
                            using (MemoryStream circularImageStream = new MemoryStream())
                            {
                                circularImage.Save(circularImageStream, System.Drawing.Imaging.ImageFormat.Png);
                                circularImageStream.Seek(0, SeekOrigin.Begin);

                                // Add image to document
                                var image = document.AddImage(circularImageStream);
                                var picture = image.CreatePicture();
                                picture.Width = 100;  // Adjust the size
                                picture.Height = 100; // Adjust the size

                                // Insert image into paragraph
                                var p = document.InsertParagraph();
                                p.AppendPicture(picture);
                            }
                        }
                    }
                }

                // Personal Information
                document.InsertParagraph("Personal Information").Font("Verdana").SpacingAfter(10).FontSize(14).Color(System.Drawing.Color.DarkCyan);
                document.InsertParagraph("Name: " + txtName.Text).Font("Verdana").FontSize(12);
                document.InsertParagraph("Email: " + txtEmail.Text).Font("Verdana").FontSize(12);
                document.InsertParagraph("Phone: " + txtPhone.Text).Font("Verdana").FontSize(12);
                document.InsertParagraph("Date of Birth: " + dpDateOfBirth.Text).Font("Verdana").FontSize(12);
                document.InsertParagraph("Wanted Job Title: " + txtWantedJobTitle.Text).Font("Verdana").SpacingAfter(10).FontSize(12);

                // Professional Summary
                document.InsertParagraph("Professional Summary").Font("Verdana").SpacingAfter(10).FontSize(14).Color(System.Drawing.Color.DarkCyan);
                AddTextToDocx(document, "Description: ", rtbProfessionalSummary, System.Drawing.Color.DarkCyan);

                // Experience
                document.InsertParagraph("Experience").Font("Verdana").SpacingAfter(10).FontSize(14).Color(System.Drawing.Color.DarkCyan);
                foreach (Grid expanderContainer in expanderExperience.Children.OfType<Grid>().Where(x => x.IsVisible))
                {
                    if (expanderContainer.Children[0] is System.Windows.Controls.Border border && border.Child is Expander expander)
                    {
                        if (expander.Content is StackPanel stackPanel)
                        {
                            var txtCompany = stackPanel.Children.OfType<TextBox>().FirstOrDefault(x => x.Name == "txtCompany");
                            var txtRole = stackPanel.Children.OfType<TextBox>().FirstOrDefault(x => x.Name == "txtRole");
                            var txtCity = stackPanel.Children.OfType<TextBox>().FirstOrDefault(x => x.Name == "txtCity");
                            var dpStartDate = stackPanel.Children.OfType<DatePicker>().FirstOrDefault(x => x.Name == "dpStartDate");
                            var dpEndDate = stackPanel.Children.OfType<DatePicker>().FirstOrDefault(x => x.Name == "dpEndDate");
                            var rtbDescription = stackPanel.Children.OfType<Grid>().FirstOrDefault()?.Children.OfType<RichTextBox>().FirstOrDefault(x => x.Name == "rtbDescription");

                            if (txtCompany != null && txtRole != null && txtCity != null && dpStartDate != null && dpEndDate != null && rtbDescription != null)
                            {
                                var experienceParagraph = document.InsertParagraph();
                                experienceParagraph.Append(txtRole.Text + ", ").Font("Verdana").FontSize(12).Bold();
                                experienceParagraph.Append(txtCompany.Text.ToUpper() + ", ").Font("Verdana").FontSize(12).Bold();
                                experienceParagraph.Append(txtCity.Text).Font("Verdana").FontSize(12).Bold();

                                var experienceDate = document.InsertParagraph();
                                experienceDate.Append("Start: ").Font("Verdana").FontSize(10).Color(System.Drawing.Color.Gray).Italic();
                                experienceDate.Append(dpStartDate.Text + " - ").Font("Verdana").FontSize(10).Color(System.Drawing.Color.Gray).Italic();
                                experienceDate.Append("End: ").Font("Verdana").FontSize(10).Color(System.Drawing.Color.Gray).Italic();
                                experienceDate.Append(dpEndDate.Text).Font("Verdana").SpacingAfter(5).FontSize(10).Color(System.Drawing.Color.Gray).Italic();

                                AddTextToDocx(document, "Description: ", rtbDescription, System.Drawing.Color.Black);
                            }
                        }
                    }
                }

                // Education
                document.InsertParagraph("Education").Font("Verdana").SpacingAfter(10).FontSize(14).Color(System.Drawing.Color.DarkCyan);
                foreach (Grid expanderContainer in expanderEducation.Children.OfType<Grid>().Where(x => x.IsVisible))
                {
                    if (expanderContainer.Children[0] is System.Windows.Controls.Border border && border.Child is Expander expander)
                    {
                        if (expander.Content is StackPanel stackPanel)
                        {
                            var txtInstitution = stackPanel.Children.OfType<TextBox>().FirstOrDefault(t => t.Name == "txtInstitution");
                            var txtDegree = stackPanel.Children.OfType<TextBox>().FirstOrDefault(t => t.Name == "txtDegree");
                            var txtCity = stackPanel.Children.OfType<TextBox>().FirstOrDefault(t => t.Name == "txtCity");
                            var dpGraduationDate = stackPanel.Children.OfType<DatePicker>().FirstOrDefault(t => t.Name == "dpGraduationDate");
                            var rtbEducationDescription = stackPanel.Children.OfType<Grid>().SelectMany(g => g.Children.OfType<RichTextBox>()).FirstOrDefault(t => t.Name == "rtbEducationDescription");

                            if (txtInstitution != null && txtDegree != null && txtCity != null && dpGraduationDate != null && rtbEducationDescription != null)
                            {
                                var educationParagraph = document.InsertParagraph();
                                educationParagraph.Append(txtDegree.Text + ", ").Font("Verdana").FontSize(12).Bold();
                                educationParagraph.Append(txtInstitution.Text.ToUpper() + ", ").Font("Verdana").FontSize(12).Bold();
                                educationParagraph.Append(txtCity.Text).Font("Verdana").FontSize(12).Bold();

                                var educationDate = document.InsertParagraph();
                                educationDate.Append("Graduation Date: ").Font("Verdana").FontSize(10).Color(System.Drawing.Color.Gray).Italic();
                                educationDate.Append(dpGraduationDate.Text).Font("Verdana").SpacingAfter(5).FontSize(10).Color(System.Drawing.Color.Gray).Italic();

                                AddTextToDocx(document, "Description: ", rtbEducationDescription, System.Drawing.Color.Black);
                            }
                        }
                    }
                }

                // Skill Section
                document.InsertParagraph("Skills").Font("Verdana").FontSize(14).Color(System.Drawing.Color.DarkCyan);
                foreach (Grid expanderContainer in ExpanderList.Children)
                {
                    if (expanderContainer.Children[0] is System.Windows.Controls.Border border && border.Child is Expander expander)
                    {
                        if (expander.Header is StackPanel headerPanel)
                        {
                            // Extract the skill name
                            TextBlock skillNameTextBlock = headerPanel.Children[0] as TextBlock;
                            string skillName = skillNameTextBlock?.Text ?? "Unknown Skill";

                            // Extract the skill level
                            TextBlock skillLevelTextBlock = headerPanel.Children[1] as TextBlock;
                            string skillLevel = skillLevelTextBlock?.Text.Replace("Level: ", "") ?? "Unknown Level";

                            // Add skill name and level to document
                            var skillParagraph = document.InsertParagraph();
                            skillParagraph.Append(skillName + " - ").Font("Verdana").FontSize(12);
                            skillParagraph.Append(skillLevel).Font("Verdana").Color(System.Drawing.Color.Gray).FontSize(12).Italic();
                        }
                    }
                }

                document.Save();
            }
        }


        private void AddTextToDocx(DocX doc, string sectionTitle, RichTextBox rtb, System.Drawing.Color black)
        {
            // Create and format the section title
            var titleParagraph = doc.InsertParagraph(sectionTitle)
                                    .Font("Verdana")
                                    .FontSize(12)
                                    .SpacingAfter(0);

            int listCounter = 1; // Counter for numbering lists

            // Loop through blocks (paragraphs) in the RichTextBox
            foreach (Block block in rtb.Document.Blocks)
            {
                if (block is System.Windows.Documents.List list)
                {
                    foreach (var listItem in list.ListItems)
                    {
                        foreach (var paragraph in listItem.Blocks.OfType<System.Windows.Documents.Paragraph>())
                        {
                            var listParagraph = doc.InsertParagraph()
                                                   .Font("Verdana")
                                                   .FontSize(12)
                                                   .SpacingAfter(10);

                            // Determine if it's a bullet or numbered list
                            if (list.MarkerStyle == TextMarkerStyle.Disc)
                            {
                                listParagraph.Append("   ").Font("Verdana").FontSize(14).Color(black); // Bullet
                                listParagraph.Append(" ").Append(GetRunText(paragraph)).FontSize(14); // Text
                            }
                            else
                            {
                                listParagraph.Append("   ").Font("Verdana").FontSize(14).Color(black); // Numbering
                                listParagraph.Append(GetRunText(paragraph)).FontSize(14); // Text
                                listCounter++;
                            }
                        }
                    }
                }
                else if (block is System.Windows.Documents.Paragraph paragraph)
                {
                    var contentParagraph = doc.InsertParagraph()
                                              .Font("Verdana")
                                              .FontSize(14)
                                              .SpacingAfter(10);

                    // Loop through the Inline elements in the paragraph
                    foreach (Inline inline in paragraph.Inlines)
                    {
                        if (inline is System.Windows.Documents.Run run)
                        {
                            // Get the text from the run
                            string runText = new TextRange(run.ContentStart, run.ContentEnd).Text;

                            // Apply formatting to the text based on the properties of the Run
                            var formattedText = contentParagraph.Append(runText).FontSize(14);

                            if (run.FontWeight == FontWeights.Bold)
                                formattedText.Bold().FontSize(14);

                            if (run.FontStyle == FontStyles.Italic)
                                formattedText.Italic().FontSize(14);

                            if (run.TextDecorations.Contains(TextDecorations.Underline[0]))
                                formattedText.UnderlineStyle(UnderlineStyle.singleLine).FontSize(14);

                            if (run.TextDecorations.Contains(TextDecorations.Strikethrough[0]))
                                formattedText.StrikeThrough(StrikeThrough.strike).FontSize(14);
                        }
                    }
                }
            }
        }

        // Helper method to extract and format text from a Paragraph
        private string GetRunText(System.Windows.Documents.Paragraph paragraph)
        {
            var text = new StringBuilder();
            foreach (Inline inline in paragraph.Inlines)
            {
                if (inline is System.Windows.Documents.Run run)
                {
                    text.Append(new TextRange(run.ContentStart, run.ContentEnd).Text);
                }
            }
            return text.ToString();
        }


        // Collapsed Watermark when the user start typing
        private void RtbProfessionalSummary_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(new TextRange(rtbProfessionalSummary.Document.ContentStart, rtbProfessionalSummary.Document.ContentEnd).Text.Trim()))
            {
                watermarkTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                watermarkTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void RtbExperienceDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(new TextRange(rtbExperienceDescription.Document.ContentStart, rtbExperienceDescription.Document.ContentEnd).Text.Trim()))
            {
                watermarkExperience.Visibility = Visibility.Visible;
            }
            else
            {
                watermarkExperience.Visibility = Visibility.Collapsed;
            }
        }

        private void RtbEducationDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(new TextRange(rtbEducationDescription.Document.ContentStart, rtbEducationDescription.Document.ContentEnd).Text.Trim()))
            {
                watermarkEducation.Visibility = Visibility.Visible;
            }
            else
            {
                watermarkEducation.Visibility = Visibility.Collapsed;
            }
        }
        // Delete expander education
        private void DeleteExpander_Click(object sender, RoutedEventArgs e)
        {
            // Show a confirmation dialog before deleting the Expander
            var result = MessageBox.Show("Are you sure you want to delete this section?",
                                         "Confirm Deletion",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Remove the container that holds the Expander and the Delete button
                if (expanderContainer != null)
                {
                    var parentPanel = expanderContainer.Parent as Panel;
                    if (parentPanel != null)
                    {
                        parentPanel.Children.Remove(expanderContainer);
                    }

                }
                btnDeleteExpander.Visibility = Visibility.Collapsed;
            }
        }

        // Delete Expander experience
        private void DeleteExpanderExperience_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var grid = (Grid)button.Parent;
            var result = MessageBox.Show("Are you sure you want to delete this experience?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var parent = (StackPanel)grid.Parent;
                parent.Children.Remove(grid);
            }
        }




        // Assemble Function to Add Expander Education details
        private void AddExpander_Click(object sender, RoutedEventArgs e)
        {
            var expanderContainer = new Grid
            {
                Margin = new Thickness(5)
            };
            expanderContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            expanderContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            var border = new System.Windows.Controls.Border
            {
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(5),
                Background = Brushes.White,
                Margin = new Thickness(0)
            };

            var expander = new Expander
            {
                Margin = new Thickness(5),
                Style = (Style)FindResource("CustomExpanderStyle"),
                HeaderTemplate = (DataTemplate)FindResource("ExpanderHeaderTemplate") // Set the HeaderTemplate here
            };

            var stackPanel = new StackPanel { Margin = new Thickness(5) };
            var txtInstitution = new TextBox { Name = "txtInstitution", Style = (Style)FindResource("CustomTextBoxStyle") };
            var txtDegree = new TextBox { Name = "txtDegree", Style = (Style)FindResource("CustomTextBoxStyle") };
            var txtCity = new TextBox { Name = "txtCity", Style = (Style)FindResource("CustomTextBoxStyle") };
            var dpGraduationDate = new DatePicker { Name = "dpGraduationDate", Margin = new Thickness(5) };

            stackPanel.Children.Add(new Label { Content = "Institution", Foreground = Brushes.Gray });
            stackPanel.Children.Add(txtInstitution);
            stackPanel.Children.Add(new Label { Content = "Degree", Foreground = Brushes.Gray });
            stackPanel.Children.Add(txtDegree);
            stackPanel.Children.Add(new Label { Content = "City", Foreground = Brushes.Gray });
            stackPanel.Children.Add(txtCity);
            stackPanel.Children.Add(new Label { Content = "Graduation Date", Foreground = Brushes.Gray });
            stackPanel.Children.Add(dpGraduationDate);
            stackPanel.Children.Add(new Label { Content = "Description", Foreground = Brushes.Gray });

            var toolBar = new ToolBar();
            AddTextFormattingButton(toolBar, "B", EditingCommands.ToggleBold, FontWeights.Bold);
            AddTextFormattingButton(toolBar, "I", EditingCommands.ToggleItalic, FontStyles.Italic);
            AddTextFormattingButton(toolBar, "U", EditingCommands.ToggleUnderline);
            AddStrikethroughButton(toolBar);
            AddTextFormattingButton(toolBar, "🔢", EditingCommands.ToggleNumbering);
            AddTextFormattingButton(toolBar, "●", EditingCommands.ToggleBullets);
            stackPanel.Children.Add(toolBar);

            var grid = new Grid
            {
                Margin = new Thickness(5, 0, 0, 0),
                Height = 100
            };

            var rtbEducationDescription = new RichTextBox
            {
                Name = "rtbEducationDescription",
                Margin = new Thickness(0),
                Padding = new Thickness(0),
                Background = (Brush)new BrushConverter().ConvertFromString("#F0F8FF"),
                BorderThickness = new Thickness(0)
            };
            rtbEducationDescription.TextChanged += RichTextEducation_TextChanged;
            var watermarkEducation = new TextBlock
            {
                Name = "watermarkEducation",
                Text = "Write your Education Summary here...",
                Foreground = Brushes.Gray,
                FontStyle = FontStyles.Italic,
                VerticalAlignment = System.Windows.VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                TextWrapping = TextWrapping.Wrap,
                IsHitTestVisible = false,
                Opacity = 0.5
            };

            grid.Children.Add(rtbEducationDescription);
            grid.Children.Add(watermarkEducation);

            stackPanel.Children.Add(grid);

            expander.Content = stackPanel;
            border.Child = expander;

            Grid.SetColumn(border, 0);

            var deleteButton = new Button
            {
                Content = "🗑️",
                FontSize = 18,
                Margin = new Thickness(0),
                VerticalAlignment = System.Windows.VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 40,
                Height = 40,
                Visibility = Visibility.Collapsed,
                Style = (Style)FindResource("DeleteButtonColor")
            };
            deleteButton.Click += DeleteNewExpander_Click;

            expanderContainer.Children.Add(border);
            expanderContainer.Children.Add(deleteButton);
            Grid.SetColumn(deleteButton, 1);

            expanderEducation.Children.Add(expanderContainer);

            expanderContainer.MouseEnter += (s, e) => deleteButton.Visibility = Visibility.Visible;
            expanderContainer.MouseLeave += (s, e) => deleteButton.Visibility = Visibility.Collapsed;

            // Set up the MultiBinding for the Expander Header
            MultiBinding headerBinding = new MultiBinding
            {
                Converter = (IMultiValueConverter)FindResource("EducationDetailsToHeaderConverter")
            };
            headerBinding.Bindings.Add(new Binding { Source = txtDegree, Path = new PropertyPath("Text") });
            headerBinding.Bindings.Add(new Binding { Source = txtInstitution, Path = new PropertyPath("Text") });
            headerBinding.Bindings.Add(new Binding { Source = txtCity, Path = new PropertyPath("Text") });
            headerBinding.Bindings.Add(new Binding { Source = dpGraduationDate, Path = new PropertyPath("Text") });

            expander.SetBinding(Expander.HeaderProperty, headerBinding);
        }


        private void Expander_MouseEnter(object sender, MouseEventArgs e)
        {
            btnDeleteExpander.Visibility = Visibility.Visible;
        }

        private void Expander_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!btnDeleteExpander.IsMouseOver)
            {
                btnDeleteExpander.Visibility = Visibility.Collapsed;
            }
        }
        private void DeleteNewExpander_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this section?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var deleteButton = sender as Button;
                var expanderContainer = deleteButton.Parent as Grid;
                expanderEducation.Children.Remove(expanderContainer);
            }
        }
        private void RichTextEducation_TextChanged(object sender, TextChangedEventArgs e)
        {
            var richTextBox = (RichTextBox)sender;
            var watermark = (TextBlock)((Grid)richTextBox.Parent).Children.OfType<TextBlock>().FirstOrDefault(tb => tb.Name == "watermarkEducation");

            if (richTextBox.Document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward) == null)
            {
                // If the document is empty, show the watermark
                watermark.Visibility = Visibility.Visible;
            }
            else
            {
                // Otherwise, hide the watermark
                watermark.Visibility = Visibility.Collapsed;
            }
        }





        // Assemble Function to Add Expander Experience details
        private void AddExperienceExpander_Click(object sender, RoutedEventArgs e)
        {
            // Create a new Grid to hold the Expander and Delete Button
            var expanderContainer = new Grid
            {
                Margin = new Thickness(5)
            };
            expanderContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            expanderContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            // Create a Border for the Expander
            var border = new System.Windows.Controls.Border
            {
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(5),
                Background = Brushes.White,
                Margin = new Thickness(0)
            };

            // Create the Expander
            var expander = new Expander
            {
                Margin = new Thickness(6),
                Style = (Style)FindResource("CustomExpanderStyle"),
                HeaderTemplate = (DataTemplate)FindResource("ExpanderHeaderTemplate") // Set the HeaderTemplate here
            };

            // Create a StackPanel to hold the content of the Expander
            var stackPanel = new StackPanel { Margin = new Thickness(5) };

            // Add content to the StackPanel
            stackPanel.Children.Add(new Label { Content = "Company" , Foreground = Brushes.Gray });
            var txtCompany = new TextBox { Name = "txtCompany", Style = (Style)FindResource("CustomTextBoxStyle") };
            stackPanel.Children.Add(txtCompany);

            stackPanel.Children.Add(new Label { Content = "Role", Foreground = Brushes.Gray });
            var txtRole = new TextBox { Name = "txtRole", Style = (Style)FindResource("CustomTextBoxStyle") };
            stackPanel.Children.Add(txtRole);

            stackPanel.Children.Add(new Label { Content = "City", Foreground = Brushes.Gray });
            var txtCity = new TextBox { Name = "txtCity", Style = (Style)FindResource("CustomTextBoxStyle") };
            stackPanel.Children.Add(txtCity);

            stackPanel.Children.Add(new Label { Content = "Start Date", Foreground = Brushes.Gray });
            var dpStartDate = new DatePicker { Name = "dpStartDate", Margin = new Thickness(5) };
            stackPanel.Children.Add(dpStartDate);

            stackPanel.Children.Add(new Label { Content = "End Date", Foreground = Brushes.Gray });
            var dpEndDate = new DatePicker { Name = "dpEndDate", Margin = new Thickness(5) };
            stackPanel.Children.Add(dpEndDate);

            stackPanel.Children.Add(new Label { Content = "Description" , Foreground = Brushes.Gray });

            // Toolbar button here...
            var toolBar = new ToolBar();
            AddTextFormattingButton(toolBar, "B", EditingCommands.ToggleBold, FontWeights.Bold);
            AddTextFormattingButton(toolBar, "I", EditingCommands.ToggleItalic, FontStyles.Italic);
            AddTextFormattingButton(toolBar, "U", EditingCommands.ToggleUnderline);
            AddStrikethroughButton(toolBar);
            AddTextFormattingButton(toolBar, "🔢", EditingCommands.ToggleNumbering);
            AddTextFormattingButton(toolBar, "●", EditingCommands.ToggleBullets);
            stackPanel.Children.Add(toolBar);

            // Create a Grid to hold the RichTextBox and watermark
            var grid = new Grid
            {
                Margin = new Thickness(5, 0, 0, 0),
                Height = 100
            };

            var rtbDescription = new RichTextBox
            {
                Name = "rtbDescription",
                Margin = new Thickness(0),
                Padding = new Thickness(0),
                Background = (Brush)new BrushConverter().ConvertFromString("#F0F8FF"),
                BorderThickness = new Thickness(0)
            };

            // Attach TextChanged event handler
            rtbDescription.TextChanged += RichTextBox_TextChanged;

            var watermark = new TextBlock
            {
                Name = "watermarkDescription",
                Text = "Write your job description here...",
                Foreground = Brushes.Gray,
                FontStyle = FontStyles.Italic,
                VerticalAlignment = System.Windows.VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                TextWrapping = TextWrapping.Wrap,
                IsHitTestVisible = false,
                Opacity = 0.5
            };

            grid.Children.Add(rtbDescription);
            grid.Children.Add(watermark);

            stackPanel.Children.Add(grid);

            expander.Content = stackPanel;
            border.Child = expander;

            Grid.SetColumn(border, 0);

            // Create the Delete Button
            var deleteButton = new Button
            {
                Content = "🗑️",
                FontSize = 18,
                Margin = new Thickness(0),
                VerticalAlignment = System.Windows.VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 40,
                Height = 40,
                Visibility = Visibility.Collapsed,
                Style = (Style)FindResource("DeleteButtonColor")
            };
            deleteButton.Click += DeleteExperienceExpander_Click;

            expanderContainer.Children.Add(border);
            expanderContainer.Children.Add(deleteButton);
            Grid.SetColumn(deleteButton, 1);

            // Add the Expander container to the Experience section
            var expanderExperience = (StackPanel)FindName("expanderExperience");
            expanderExperience.Children.Add(expanderContainer);

            // Show the delete button on mouse hover
            expanderContainer.MouseEnter += (s, e) => deleteButton.Visibility = Visibility.Visible;
            expanderContainer.MouseLeave += (s, e) => deleteButton.Visibility = Visibility.Collapsed;

            // Set the Expander header binding
            MultiBinding headerBinding = new MultiBinding
            {
                Converter = (IMultiValueConverter)FindResource("ExperienceDetailsToHeaderConverter")
            };
            headerBinding.Bindings.Add(new Binding { Source = txtRole, Path = new PropertyPath("Text") });
            headerBinding.Bindings.Add(new Binding { Source = txtCompany, Path = new PropertyPath("Text") });
            headerBinding.Bindings.Add(new Binding { Source = txtCity, Path = new PropertyPath("Text") });
            headerBinding.Bindings.Add(new Binding { Source = dpStartDate, Path = new PropertyPath("Text") });
            headerBinding.Bindings.Add(new Binding { Source = dpEndDate, Path = new PropertyPath("Text") });

            expander.SetBinding(Expander.HeaderProperty, headerBinding);

            // Bind the Expander header to the template properties
        }


        // Function to hide watermark when start typing
        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var richTextBox = (RichTextBox)sender;
            var watermark = (TextBlock)((Grid)richTextBox.Parent).Children.OfType<TextBlock>().FirstOrDefault(tb => tb.Name == "watermarkDescription");

            if (richTextBox.Document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward) == null)
            {
                // If the document is empty, show the watermark
                watermark.Visibility = Visibility.Visible;
            }
            else
            {
                // Otherwise, hide the watermark
                watermark.Visibility = Visibility.Collapsed;
            }
        }

        // When mouse on expander container show delete button
        private void ExpanderContainer_MouseEnter(object sender, MouseEventArgs e)
        {
            var container = (Grid)sender;
            var deleteButton = (Button)container.FindName("btnDeleteExperienceExpander");
            if (deleteButton != null)
            {
                deleteButton.Visibility = Visibility.Visible;
            }
        }
        // When mouse leave from expander container hide delete button
        private void ExpanderContainer_MouseLeave(object sender, MouseEventArgs e)
        {
            var container = (Grid)sender;
            var deleteButton = (Button)container.FindName("btnDeleteExperienceExpander");
            if (deleteButton != null)
            {
                deleteButton.Visibility = Visibility.Collapsed;
            }
        }
        // Show delete button when mouse over delete button place
        private void DeleteExperienceExpander_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var expanderContainer = (Grid)button.Parent;
            var result = MessageBox.Show("Are you sure you want to delete this experience?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                expanderExperience.Children.Remove(expanderContainer);
            }
        }
        // Activate button in toolbar
        private void AddTextFormattingButton(ToolBar toolBar, string content, RoutedCommand command, object parameter = null)
        {
            var button = new Button
            {
                Content = content,
                Command = command,
                CommandParameter = parameter,
                FontWeight = parameter is FontWeight weight ? weight : FontWeights.Normal,
                FontStyle = parameter is FontStyle style ? style : FontStyles.Normal,
                Padding = new Thickness(5)
            };
            toolBar.Items.Add(button);
        }
        // Add Strikethrough to toolbar
        private void AddStrikethroughButton(ToolBar toolBar)
        {
            var button = new Button
            {
                Content = "S",
                FontStyle = FontStyles.Normal
            };
            button.Click += (s, e) =>
            {
                var rtb = GetRichTextBoxFromToolbar(toolBar);
                if (rtb != null)
                {
                    TextRange textRange = new TextRange(rtb.Selection.Start, rtb.Selection.End);
                    var currentTextDecorations = textRange.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection;

                    if (currentTextDecorations != null && currentTextDecorations.Contains(TextDecorations.Strikethrough[0]))
                    {
                        // Remove strikethrough
                        textRange.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                    }
                    else
                    {
                        // Add strikethrough
                        var strikethrough = new TextDecorationCollection(TextDecorations.Strikethrough);
                        textRange.ApplyPropertyValue(Inline.TextDecorationsProperty, strikethrough);
                    }
                }
            };
            toolBar.Items.Add(button);
        }

        // Helper method to get the RichTextBox from the toolbar's parent container
        private RichTextBox GetRichTextBoxFromToolbar(ToolBar toolBar)
        {
            var stackPanel = toolBar.Parent as StackPanel;
            if (stackPanel != null)
            {
                return stackPanel.Children.OfType<Grid>().FirstOrDefault()?.Children.OfType<RichTextBox>().FirstOrDefault();
            }
            return null;
        }


        // Helper method for TabItem Skill
        private void SkillTab_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (SkillsList.Items.Count == 0)
            {
                PopulateSkills();
            }
        }
        private void PopulateSkills()
        {
            List<string> skills = new List<string>
            {
                "Creativity ",
                "Teamwork ",
                "Leadership and Teamwork ",
                "Analytical Skill ",
                "Flexibility and Adaptability ",
                "Good Communication ",
                "Project Management Skills ",
                "Effective Time Management ",
                "Problem Solving ",
                "Initiative and Problem solving abilities ",
                "Team Player ",
                "Ability to Learn Quickly ",
                "Ability to Multitask ",
                "Hightly Organized ",
                "Critical Thinking ",
                "Bussiness Development ",
                "Strong Communication Skill "
            };

            SkillsList.Items.Clear();

            foreach (var skill in skills)
            {
                // Add "+" symbol before each skill name
                Button skillButton = new Button
                {
                    Content = $"{skill} +",  // Prepend "+" symbol to skill name
                    Style = (Style)FindResource("SkillButtonStyle")
                };

                skillButton.Click += SkillButton_Click;
                SkillsList.Items.Add(skillButton);
            }
        }
        private void SkillButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                SkillsList.Items.Remove(clickedButton);

                string skill = clickedButton.Content.ToString().Remove(clickedButton.Content.ToString().Length - 2);
                // Remove the "+ "

                // Create a container for the expander and the delete button
                Grid expanderContainer = new Grid
                {
                    Margin = new Thickness(5)
                };
                expanderContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                expanderContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                // Create the Expander and apply a box-like border appearance
                Expander expander = new Expander
                {
                    Margin = new Thickness(0)
                };

                System.Windows.Controls.Border expanderBorder = new System.Windows.Controls.Border
                {
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(10),
                    Background = Brushes.White,
                    Padding = new Thickness(10),
                    
                };

                // Create a StackPanel for the Expander header
                StackPanel headerPanel = new StackPanel
                {
                    Orientation = System.Windows.Controls.Orientation.Vertical
                };

                TextBlock skillNameTextBlock = new TextBlock
                {
                    Text = skill,
                    FontWeight = FontWeights.Bold,
                    FontSize = 14
                };

                TextBlock skillLevelTextBlock = new TextBlock
                {
                    Text = "Level: Novice",
                    FontStyle = FontStyles.Italic,
                    FontSize = 12
                };

                headerPanel.Children.Add(skillNameTextBlock);
                headerPanel.Children.Add(skillLevelTextBlock);

                expander.Header = headerPanel;

                Grid grid = new Grid
                {
                    Margin = new Thickness(10)
                };

                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                TextBlock levelLabel = new TextBlock
                {
                    Text = "Adjust Level:",
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 0, 5, 0)
                };
                Grid.SetRow(levelLabel, 0);
                Grid.SetColumn(levelLabel, 0);
                grid.Children.Add(levelLabel);

                Slider levelSlider = new Slider
                {
                    Minimum = 1,
                    Maximum = 5,
                    Value = 1,
                    TickFrequency = 1,
                    IsSnapToTickEnabled = true,
                    Width = 150,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                Grid.SetRow(levelSlider, 0);
                Grid.SetColumn(levelSlider, 1);
                grid.Children.Add(levelSlider);

                TextBlock levelValueTextBlock = new TextBlock
                {
                    Text = MapSkillLevelToLabel((int)levelSlider.Value),
                    Margin = new Thickness(5, 0, 0, 0),
                    VerticalAlignment = System.Windows.VerticalAlignment.Center
                };
                Grid.SetRow(levelValueTextBlock, 0);
                Grid.SetColumn(levelValueTextBlock, 2);
                grid.Children.Add(levelValueTextBlock);

                levelSlider.ValueChanged += (s, ev) =>
                {
                    string skillLevelLabel = MapSkillLevelToLabel((int)levelSlider.Value);
                    levelValueTextBlock.Text = skillLevelLabel;
                    skillLevelTextBlock.Text = $"Level: {skillLevelLabel}";
                };

                expander.Content = grid;

                // Set the Expander as the content of the Border
                expanderBorder.Child = expander;

                // Add the Expander border to the first column of the grid
                Grid.SetColumn(expanderBorder, 0);
                expanderContainer.Children.Add(expanderBorder);

                // Add a delete button outside the expander box, in the second column
                Button deleteButton = new Button
                {
                    Content = "🗑",  // Unicode symbol for a trash can
                    Background = Brushes.Transparent,
                    Foreground = Brushes.Red,
                    BorderBrush = Brushes.Transparent,
                    FontSize = 18,
                    ToolTip = "Delete Skill",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    Padding = new Thickness(5),
                    Cursor = Cursors.Hand,
                    Visibility = Visibility.Collapsed,
                    Style = (Style)FindResource("DeleteButtonColor")

                };

                deleteButton.Click += (s, ev) => DeleteExpander(expanderContainer,skill);

                // Show delete button when mouse is over the expander container
                expanderContainer.MouseEnter += (s, ev) => deleteButton.Visibility = Visibility.Visible;
                expanderContainer.MouseLeave += (s, ev) => deleteButton.Visibility = Visibility.Collapsed;

                Grid.SetColumn(deleteButton, 1);
                expanderContainer.Children.Add(deleteButton);

                ExpanderList.Children.Add(expanderContainer);
            }
        }

        private void DeleteExpander(Grid expanderContainer, string skill)
        {
            // Remove the expander container from the parent stack panel
            ExpanderList.Children.Remove(expanderContainer);
            Button restoredSkillButton = new Button
            {
                Content = $"{skill} +",
                Style = (Style)FindResource("SkillButtonStyle")
            };

            restoredSkillButton.Click += SkillButton_Click;

            SkillsList.Items.Add(restoredSkillButton);
        }

        private string MapSkillLevelToLabel(int level)
        {
            switch (level)
            {
                case 1:
                    return "Novice";
                case 2:
                    return "Beginner";
                case 3:
                    return "Skillful";
                case 4:
                    return "Experienced";
                case 5:
                    return "Expert";
                default:
                    return "Unknown";
            }
        }
    }
}

