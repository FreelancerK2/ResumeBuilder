   M S B A M L   `   `   `  ����H  DResumeBuilder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ResumeBuilderResumeBuilder  5   @       ResumeBuilder.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -localclr-namespace:ResumeBuilder     6@     � Title$  Resume Builder��5   	   $	��450��6    $	��800��6-    � 	Resources 5      5   /ResumeBuilder.EducationDetailsToHeaderConverter %  !EducationDetailsToHeaderConverter6   0ResumeBuilder.ExperienceDetailsToHeaderConverter & "ExperienceDetailsToHeaderConverter  ExpanderHeaderTemplate �� 
TargetType  CustomExpanderStyle �� Property �� Value  NavigationButtonStyle �� 
TargetType �� CornerRadius �� Padding �� Triggers	 P� Property
 P� Value  DeleteButtonColor  DeleteButtonStyle  CustomTextBoxStyle  WatermarkTextBlockStyle 	 LargeIconButtonStyle ��Name J� Opacity ��Name �� Triggers �� 
TargetName 
 SkillButtonStyle  ModernTabControlStyle��%  &	        &	      &	 &     &	 �     7  &	 '    &	 �    &	 s    &	     &	 M
    &		     &	
 C    &	 �      5	   
   62     5
   
   63   �� 5   
   .R�5      �� .+�5      �� $
��Bold"�66   ��6   �� *
MainText+66   �� 5      $��Italic$�66   
���������6   �� *
DateText+6I   5      5      �� 5   
   " 9�6   .�5      ��  � ��8   6/   5      �� 5   
   " ��6.   .�5      ��  � �� �#007ACC6+   �� 5       � ��
 �����6+   �� 5       � ��$
 Bold"�6+   �� 5       � ��$
 10,5q�6(   �� 5       � ��$ 5q�6'   �� 5       � ��$ 0q�60   �� 5       � ��$ Center=�64   �� 5        � ��$ Center=�62   �� 5!       � �� 5"      �� 5#      " ��6&   .R�5$      �� #��z��6!   $ 5��5%   !   # z��5&   !   ._�5'      �� $��Center=�6/   $��Center=�5(   /   #��z��5)   /   5*      5+      5-       5.      P� 5/      	 �@J�IsMouseOver$

 True��61   . �50      ��  � �� �#005B9963   51      53      �� 55   
   " ��6*   .�56      ��  � ��
 �   �6+   �� 57       � ��
 ���� 6+   �� 58       � ��$ 0q�60   �� 59       � �� 5:      �� 5;      " ��6&   .R�5<      �� ._�5=      �� $��Center=�6/   $��Center=�5>   #   5?      5@      5B       5C      P� 5D      	 �@J�IsMouseOver$

 True��61   . �5E      ��  � ��
 �  ��63   5F      5H      �� 5K   
   " ��6*   .�5L      ��  � �� �#FF00006+   �� 5M       � ��
 �����6+   �� 5N       � ��$
 Bold"�6+   �� 5O       � ��$
 10,5q�6(   �� 5P       � ��$ 5q�6'   �� 5Q       � ��$ 0q�60   �� 5R       � ��$ Center=�64   �� 5S       � ��$ Center=�62   �� 5T       � �� 5U      �� 5V      " ��6&   .R�5W      �� #��z��6!   $ 5��5X   !   # z��5Y   !   ._�5Z      �� $��Center=�6/   $��Center=�5[   /   #��z��5\   /   5]      5^      5`       5a      P� 5b      	 �@J�IsMouseOver$

 True��61   . �5c      ��  � �� �#FF333363   5d      5f      �� 5i   
   " ��6+   .�5j      ��  � �� �#F0F8FF6+   �� 5l       � �� �#0000006+   �� 5n       � ��
 ���� 6,   �� 5p       � ��$ 0q�60   �� 5r       � ��$ 5q�6(   �� 5s       � ��$ 5q�6'   �� 5t       � ��$ 14(�6)   �� 5u       � ��$ 30��6'    5v      P� 5w      	 �@J�IsMouseOver$

 True��61   . �5x      ��  � �� �#D3EFFF63   5z      P� 5{      	 �@J�IsKeyboardFocused$

 True��67   . �5|      ��  � �� �#B0E0E663   5~      5�      �� 5�   
   " ��60   .�5�      ��  � ��
 �����6+   �� 5�       � ��$ Italic$�6*   �� 5�       � ��$ 5,0,0,0q�6'   �� 5�       � ��$ Center=�62   5�      �� 5�   
   " ��6-   .�5�      ��  � ��
 ���� 6+   �� 5�       � ��$
 Bold"�6+   �� 5�       � ��$
 10,5q�6(   �� 5�       � ��$ 5q�6'   �� 5�       � ��$ 0q�60   �� 5�       � ��$ Center=�64   �� 5�       � ��$ Center=�62   �� 5�       � �� 5�      �� 5�      " ��6&   .R�5�      � .+�5�      �� $ border��6%   #��z��65   $	 0.7Q�6_   $ 5��6m   �� 5�      $
 icon��6(   $��80(�66   $
��👤��6D   $��Center=�6N   $��Center=�6k   5�       5�      P� 5�      	 �@J�IsMouseOver$

 True��6=   . �5�   "   �� $ border��6)    � �� �#005B996S   5�      P� 5�      	 � ��$

 True��6;   . �5�   "   �� $ border��6)    � �� �#A0C4FF6S   5�      5�      5�      5�      �� 5�   
   " ��6)   .�5�      ��  � �� �#F0F8FF6+   �� 5�       � ��
 �   �6+   �� 5�       � ��$ 16(�6)   �� 5�       � ��$
 10,5q�6(   �� 5�       � ��$ 5q�6'   �� 5�       � ��
 ���� 6,   �� 5�       � ��$ 2q�60   �� 5�       � ��$
 Left=�64   �� 5�       � ��$ Center=�62   �� 5�       �@�Cursor$
 Hand��6'   �� 5�       � ��$
 auto��6&   �� 5�       � ��$ 35��6'   �� 5�       � ��$ 80��6)   �� 5�       � �� 5�      �� 5�      " ��6&   .R�5�      �� #��z��6!   #��z��5�   !   #��z��5�   !   $ 4��5�   !   ._�5�      �� $��Center=�6/   $��Center=�5�   /   #��z��5�   /   5�      5�      5�       5�      P� 5�      	 �@J�IsMouseOver$

 True��61   . �5�      ��  � �� �#7bc2d663   5�      5�      �� 5�   
   " ��6.   .�5�      ��  � ��
 �����6+   �� 5�       � ��
 �����6,   �� 5�       � ��$ 1q�60   �� 5�       � ��$ 10q�6'   �� 5�       � ��$ 5q�6(   �� 5�       � �� 5�      �� 5�      " ��6   .�5�      ��  � ��
 �����67   �� 5�       � ��
 ���� 67   �� 5�       � ��
 ���� 68   �� 5�       � ��$ 0q�6<   �� 5�       � ��$
 10,5q�64   �� 5�       � ��$ 14(�65   �� 5�       � ��$ 0,0,0,5q�63   �� 5�       � �� 5�      �� 5�   "   " ��62   .R�5�   &   �� #��z��6-   #��z��5�   -   #��z��5�   -   $ 5��5�   -   $ 10q�5�   -   ._�5�   *   �� $��Center=�6;   $��Center=�5�   ;   $��Header��5�   ;   5�   '   5�   #   5�       5�      P� 5�      	 �@��
IsSelected$

 True��6<   . �5�   "   ��  � ��
 �����6?   �� 5�   "    � ��
 ��� �6?   5�      5�      5�      5�      5�      .��5      � .+�5  
   ]� .+�5     c� $	��Top=�6   .@�5     o� .@�5     m� $
��File��6   .@�5     m� -   5        $��Save as PDF��5  #   68   m� 5     -   5        $��Save as DOCX��5  #   69   m� 5     -   5        $��Save as DOC��5  #   68   5	     5
     5     �� 5     -   5         ��Name$ 
tabControl��5     #��[ 6-   .@�5     �� $��Personal Information��6   .��5     � $��10q�6    � RowDefinitions 5     �� 5     $��* �6,   �� 5     $
��Auto �6,    � ColumnDefinitions 5     �� 5     $	��200 �6/   �� 5     $��* �6/   .+�5     �� $��0��6%   $��10q�65   .+�5     �� $��Photo��6$   � 5     .+�5  "   �� -   5         ��Name$ imgPhoto��5  (   $	��100��6:   $	��115��6G   $��0q�6S    J� Clip 5  &   �� 5  *    �� RadiusX$ 10Q�6<    �� RadiusY$ 10Q�6I    �� Rect$ 5,0,100,100�6V   5   #   �� 5!  "   -   5         ��Name$ btnUploadPhoto��5!  )   #��[	 6[   $	��100��5"  )   $	��100��65   $	 0.7Q�6B   $��Center=�5#  )   $��Center=�6F   
�������5%  #   5&     �� 5'      �� Orientation$ Vertical=�6)   $��5q�6@   .+�5(  "   �� -   5        $��Upload Photo��5(  )   #��[ 6Z   $��5q�6�   �� 5)  "   -   5        $
��Edit��5)  )   #��[ 6P   $��5q�6   �� 5*  "   -	   5        $ btnDeletePhoto��5*  )   $��Delete��6A   #��[ 6l   $��5q�6�   $y�	Collapsed=�6�   5+     5,     �� 5.     $��1��6%   $��10q�65   .+�5/     �� $
��Name��6$   
�������63   �� 50     -
   5         ��Name$ txtName��50  &   #��[ 67   �� 51     $��Email��6$   
�������64   �� 52     -   5        $ txtEmail��52  &   #��[ 68   �� 53     $��Phone��6$   
�������64   �� 54     -   5        $ txtPhone��54  &   #��[ 68   �� 55     $��Date of Birth��6$   
�������6<   (  "System.Windows.Controls.DatePicker  56     -   5          Name$ dpDateOfBirth��56  )   $��5q�6@   �� 57     $��Wanted Job Title��6$   
�������6?   �� 58     -   5        $ txtWantedJobTitle��58  &   #��[ 6A   59     �� 5<     $��1��6%   $��2��62   $ 
Horizontal=�6F   $��Right=�6_   $��5q�6{   .+�5=     �� -   5        $��Previous��5=  %   #��[ 6O   �� 5>     -   5        $
��Next��5>  %   #��[ 6G   $��5,0,0,0q�6v   5?     5@     5A     �� 5C     $��Professional Summary��6   .��5D     �� $��10q�6!   .+�5E     �� -   5        $ txtPlaceholder��5E  $   $����Write 2-4 short, energetic sentences about how great you are. Mention the role and what you did. What were the big achievements? Describe your motivation and list your skills.��5F      
�������$��10q�5G  2   $	��Top=�6>   $
��Left=�6V   $
��Wrap=�5H       J� IsHitTestVisible$ False��5I      $��Italic$�5J      �� 5L     $��Description��6    c� 5M     .@�5N     d� -   5        $��B��5N  +   $
��Bold"�67   6I   d� 5O     -   5        $��I��5O  +   $��Italic$�67   6J   d� 5P     -   5        $��U��5P  +   67   d� 5Q     -   5        $��S��5Q  +   67   d� 5R     -   5        $
��🔢��5R  +   68   d� 5S     -   5        $	��●��5S  +   67   5T     � 5U     $��5,0,0,0q�6   $	��100��60   .+�5V     �� -   5         ��Name$ rtbProfessionalSummary��5V  *   $��0q�6J   $��0q�6U   ���#F0F8FF$��0q�6v   �� 5W     -   5        $ watermarkTextBlock��5W  (   $-��'Write your Professional Summary here...��5X  (   
�������$��Italic$�5Z  (   $	��Top=�5[  (   $
��Left=�5\  (   $
��Wrap=�5]  (   $ False��5^  (   $	 0.5Q�5_  (   5`     �� 5b     $ 
Horizontal=�6%   $��Right=�6>   $��5q�6Z   .+�5c     �� -   5        $��Previous��5c  %   #��[ 6O   �� 5d     -   5        $
��Next��5d  %   #��[ 6G   5e     5f     5g     �� 5h     $��
Experience��6   .��5i     �� -   5         ��Name$ ExperienceExpanderList��5i  !   $��10q�6A   .+�5j     �� -   5        $ txtExperience��5j  $   $k��eA varied experience on your resume sums up the value that your work and background will bring to job.��5k  $   
�������$��10q�5l  6   $	��Top=�6B   $
��Left=�6Z   $
��Wrap=�5m  $   $ False��5n  $   $��Italic$�5o  $   �� 5p     -   5        $ expanderExperience��5p  %   $��5q�6A   � 5q     -   5         �Name$! expanderContainerExperience��5q     $
��10,0q�6D    5s     �� 5t  "   $��* �63   �� 5u  "   $
��Auto �63   .+�5w     �� 
�������$��1q�68   $ 5��6L   
�������$��0��6p   ._�5x  "   9� #��[ 6,   $��5q�6Y   ��5y  &   �� 5z  *     �� ResourceKey$  ExpanderHeaderTemplate��69   ��5|  &   `� 5}  *   ! `� 	Converter#! [ 67   .1�5~  .   �� " �� ElementName$" txtRole��66   # �� Path$
# Text�6L   �� 5  .   $" 
txtCompany��66   $
# Text�6O   �� 5�  .   $" txtCity��66   $
# Text�6L   �� 5�  .   $" dpStartDate��66   $
# Text�6P   �� 5�  .   $" 	dpEndDate��66   $
# Text�6N   5�  +   .��5�  &   �� -    5        $ rtbDescription��5�  1   $��5q�6I   .+�5�  *   �� $��Company Name��60   
�������6G   �� 5�  *   -!   5        $ 
txtCompany��5�  2   #��[ 6F   �� 5�  *   $
��Role��60   
�������6?   �� 5�  *   -"   5        $ txtRole��5�  2   #��[ 6C   �� 5�  *   $
��City��60   
�������6?   �� 5�  *   -#   5        $ txtCity��5�  2   #��[ 6C   �� 5�  *   $��
Start Date��60   
�������6E     5�  *   -$   5        $ dpStartDate��5�  5   $��5q�6J   �� 5�  *   $��End Date��60   
�������6C     5�  *   -%   5        $ 	dpEndDate��5�  5   $��5q�6H   �� 5�  *   $��Description��60   
�������6F   c� 5�  *   .@�5�  .   d� -&   5        $��B��5�  ;   $
��Bold"�6G   6Y   d� 5�  .   -'   5        $��I��5�  ;   $��Italic$�6G   6Z   d� 5�  .   -(   5        $��U��5�  ;   6G   d� 5�  .   -)   5        $��S��5�  ;   6G   d� 5�  .   -*   5        $
��🔢��5�  ;   6H   d� 5�  .   -+   5        $	��●��5�  ;   6G   5�  +   � 5�  *   $��5,0,0,0q�6/   $	��100��6@   .+�5�  .   �� -,   5        $ rtbExperienceDescription��5�  :   $��0q�6\   $��0q�6g   ���#F0F8FF$��0q�6�   �� 5�  .   --   5        $ watermarkExperience��5�  8   $+��%Write your Experience Summary here...��5�  8   
�������$��Italic$�5�  8   $	��Top=�5�  8   $
��Left=�5�  8   $
��Wrap=�5�  8   $ False��5�  8   $	 0.5Q�5�  8   5�  +   5�  '   5�  #   5�     �� 5�     -.   5        $! btnDeleteExperienceExpander��5�  %   $
��🗑��6J   #��[ 6~   $��0q�6�   $��18(�6�   $	��Top=�6�   $��Right=�6�   $��1��6�   $��40��6  $��40��6  $y�	Collapsed=�6  5�     �� 5�     -/   5        $��+ Add More Experience��5�  !   $��	10,0,10,0q�6e   $��Stretch=�6x   
������ $��0q�6�   $��14(�6�   ���#193355$��Medium"�6�   �� 5�     $ 
Horizontal=�6%   $��Right=�6>   $��5q�6Z   .+�5�     �� -0   5        $��Previous��5�  %   #��[ 6O   $��5,0,0,0q�6~   �� 5�     -1   5        $
��Next��5�  %   #��[ 6G   5�     5�     5�     �� 5�     $��	Education��6   .��5�     �� -2   5        $ EducationExpanderList��5�  !   $��10q�6@   .+�5�     �� -3   5        $ txtEducation��5�  $   $o��iA varied education on your resume sume up the value that your learnings and background will bring to job.��5�  $   
�������$��10q�5�  6   $	��Top=�6B   $
��Left=�6Z   $
��Wrap=�5�  $   $ False��5�  $   $��Italic$�5�  $   �� 5�     -4   5        $ expanderEducation��5�  %   $��5q�6@   � 5�     -5   5        $ expanderContainer��5�     $
��10,0q�6:    5�     �� 5�  "   $��* �63   �� 5�  "   $
��Auto �63   .+�5�     �� 
�������$��1q�68   $ 5��6L   
�������$��0��6p   ._�5�  "   9� -6   5        $ 9�Name$$ expanderExperienceDetails��5�  +   #��[ 6O   $��5q�6|   ��5�  &   �� 5�  *   $  ExpanderHeaderTemplate��69   ��5�  &   `� 5�  *   #! [  67   .1�5�  .   �� $" 	txtDegree��66   $
# Text�6N   �� 5�  .   $" txtInstitution��66   $
# Text�6S   �� 5�  .   $" textCity��66   $
# Text�6M   �� 5�  .   $" dpGraduationDate��66   $
# Text�6V   5�  +   .��5�  &   �� $��5q�61   .+�5�  *   �� $��Institution��60   
�������6F   �� 5�  *   -7   5        $ txtInstitution��5�  2   #��[ 6J   �� 5�  *   $��Degree��60   
�������6A   �� 5�  *   -8   5        $ 	txtDegree��5�  2   #��[ 6E   �� 5�  *   $
��City��60   
�������6?   �� 5�  *   -9   5        $ textCity��5�  2   #��[ 6D   �� 5�  *   $��Graduation Date��60   
�������6J     5�  *   -:   5        $ dpGraduationDate��5�  5   $��5q�6O   �� 5�  *   $��Description��60   
�������6F   c� 5�  *   .@�5�  .   d� -;   5        $��B��5�  ;   $
��Bold"�6G   6Y   d� 5�  .   -<   5        $��I��5�  ;   $��Italic$�6G   6Z   d� 5�  .   -=   5        $��U��5�  ;   6G   d� 5�  .   ->   5        $��S��5�  ;   6G   d� 5�  .   -?   5        $
��🔢��5�  ;   6H   d� 5�  .   -@   5        $	��●��5�  ;   6G   5�  +   � 5�  *   $��5,0,0,0q�6/   $	��100��6@   .+�5�  .   �� -A   5        $ rtbEducationDescription��5�  :   $��0q�6[   $��0q�6f   ���#F0F8FF$��0q�6�   �� 5�  .   -B   5        $ watermarkEducation��5�  8   $*��$Write your Education Summary here...��5�  8   
�������$��Italic$�5�  8   $	��Top=�5�  8   $
��Left=�5�  8   $
��Wrap=�5�  8   $ False��5�  8   $	 0.5Q�5�  8   5�  +   5�  '   5�  #   5�     �� 5�     -C   5        $ btnDeleteExpander��5�  %   $
��🗑��6@   #��[ 6j   $��0q�6�   $��18(�6�   $	��Top=�6�   $��Right=�6�   $��1��6�   $��40��6�   $��40��6�   $y�	Collapsed=�6	  5�     �� 5�     -D   5        $��+ Add More Education��5�  !   $��	10,0,10,0q�6[   $��Stretch=�6n   
������ $��0q�6�   $��14(�6�   ���#193355$��Medium"�6�   �� 5�     $ 
Horizontal=�6%   $��Right=�6>   $��5q�6Z   .+�5�     �� -E   5        $��Previous��5�  %   #��[ 6O   $��5,0,0,0q�6~   �� 5�     -F   5        $
��Next��5�  %   #��[ 6G   5      5     5     �� 5     -G   5        % ��Name$% SkillTab��5  )   $��Skill��6   .��5     �� .+�5     �� -H   5        $ txtSkill��5  $   $����Choose 5 inportant skills that show you fit the position. Make sure they match the key skills mentioned in the job listing(especially when applying via an online system).��5  $   
�������$��10q�5  6   $	��Top=�6B   $
��Left=�6Z   $
��Wrap=�5	  $   $ False��5
  $   $��Italic$�5  $   �� 5     -I   5        & ��Name$& 
SkillsList��5  '   $��10q�6;   ��5     �� 5  "   .R�5  &   � ' � Orientation$' 
Horizontal=�60   5  #   5     �� 5     -J   5        $ ExpanderList��5  %   $��10q�6;   �� 5     $ 
Horizontal=�6%   $��Right=�6>   $��5q�6Z   .+�5     �� -K   5        $��Previous��5  %   #��[ 6O   $��5,0,0,0q�6~   5     5     5     5     5     5     5      