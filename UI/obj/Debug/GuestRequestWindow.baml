   M S B A M L   `   `   `  ����=  9UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:UIUI  5   	       UI.GuestRequestWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   "localclr-namespace:UI     5   	     � Title$-  'נופש חופש - בקשת אירוח��5   	   $��505.5��6(   $��846.5��67   $��16(�6E    � WindowStartupLocation$ CenterScreen=�6S    � 	Resources 5	      !   UI.EnumDescriptionConverter   enumConverter��%   &	          5
   
   6)   5      .��5      � $��0,0,0,-3q�6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��1.5* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   ��  �� TextAlignment$ Center=�6   $��0,0,10,0q�6+   $
��Wrap=�6=   $��Bottom=�6Q   $��64��6l   $��36(�6x   $
��Bold"�6�   
�������$��2��6�   �� 6�   $.�(?איזה חופשה אתם מחפשים��6�   �� 6�   5       6#   �� 5!   
   $
��Left=�6   $ Center=�6/   $��בחר אזור בארץ��6F   $��	267,0,0,0q�6[   $��5��6n   $
��Wrap=�6{   $��Bottom=�6�   $	��142��6�   $��31��6�   $��1��6�   $
��Bold"�6�   $��20(�6�   ���	#FF792C8F6�   �� 5%   
   $
��Left=�6   $ Center=�6/   $��בחר סוג חופשה��6F   $��	262,0,0,0q�6[   $��6��6n   $
��Wrap=�6{   $��Bottom=�6�   $	��147��6�   $��31��6�   $��1��6�   $
��Bold"�6�   $��20(�6�   ���	#FF792C8F6�   �� 5&   
   -   5        $��Adults��5&      $
��Left=�6    $��0��6;    �� 	MaxLength$ 1��6K   $��2��6Y   $��38��6f   $��34��6q   $��Bottom=�6}   $��	258,0,0,0q�6�   �� 5'   
   -   5        $��	Childrens��5'      $
��Left=�6#   $��0��6>   $ 1��6N   $��34��6\   $��	23,22,0,0q�6h   $��2��6{    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $��34��6�   �� 5(   
   $
��Left=�6   $��	301,0,0,0q�6/   $��2��6B   $��0��6O   $
��Wrap=�6_   $��Bottom=�6s   $��93��6�   $��34��6�   ���	#FF2814A4$
��Bold"�6�   $��18(�6�   �� 6�   $�מבוגרים��6�   �� 6�   5*      6#   �� 5+   
   $
��Left=�6   $��62,0,0,0q�6/   $��0��6A   $��2��6Q   $
��Wrap=�6^   $��
ילדים��6r   $��Bottom=�6   $��34��6�   $	��120��6�   ���	#FF2814A4$
��Bold"�6�   $��18(�6�   (  "System.Windows.Controls.DatePicker  5,   
   -   5        $��	EntryDate��5,      $��1��6&   $
��Left=�66   $��	143,0,0,0q�6Q   $��1��6d   $	��Top=�6q   $	��172��6�   $��32��6�   �� 5-   
   $��1��6   $
��Left=�6$   $ Center=�6?   $��	320,0,0,0q�6V   $��1��6i   $
��Wrap=�6v   $��צ'ק-אין��6�   $	��Top=�6�   $��89��6�   $��46��6�   ���	#FF4444EE$��20(�6�   $
��Bold"�6�     5.   
   -   5        $��ReleaseDate��5.      $��0��6(   $
��Left=�68   $��	126,0,0,0q�6S   $��1��6f   $	��Top=�6s   $	��170��6�   $��32��6�   �� 5/   
   $��0��6   $
��Left=�6$   $ Center=�6?   $��	301,0,0,0q�6V   $��1��6i   $
��Wrap=�6v   $��צ'ק-אאוט��6�   $	��Top=�6�   $	��108��6�   $��46��6�   ���	#FF4444EE$��20(�6�   $
��Bold"�6�   �� 50   
   -   5        $
��pool��50      $
��Left=�6!   $��78��6<   $��23,0,0,0q�6H   $��3��6Z   $	��Top=�6g   $��76��6   $��2��6�   .+�51      	� -   5        $��YesPool��51      $
��כן��6)   $��0,0,10,0q�66   	� 52      -   5        $��NoPool��52      $
��לא��6(   $��0,0,10,0q�65   	� 53      -   5        $��PossiblePool��53      $��
אפשרי��6.   $��28��6>    d� 	IsChecked$
 TrueY�6J   54      �� 55   
   $
��Left=�6   $��
126,10,0,0q�6/   $��3��6C   $
��Wrap=�6P   $��
בריכה��6d   $	��Top=�6q   $��69��6�   $��37��6�   $
��Bold"�6�   $��18(�6�   ���	#FF2814A46�   �� 56   
   -	   5        $��Jaccuzi��56      $
��Left=�6$   $��78��6?   $��	220,0,0,0q�6K   $��3��6^   $	��Top=�6k   $��76��6�   $��2��6�   .+�57      	� -
   5        $��
YesJaccuzi��57      $
��כן��6,   $��0,0,10,0q�69   	� 58      -   5        $��	NoJaccuzi��58      $
��לא��6+   $��0,0,10,0q�68   	� 59      -   5        $��PossibleJaccuzi��59      $��
אפשרי��61   $��32��6A   $
 TrueY�6M   5:      �� 5;   
   $
��Left=�6   $��
344,10,0,0q�6/   $��3��6C   $
��Wrap=�6P   $��ג'קוזי��6d   $	��Top=�6r   $��65��6�   $��37��6�   $
��Bold"�6�   $��18(�6�   ���	#FF2814A46�   �� 5<   
   -   5        $��garden��5<      $
��Left=�6#   $��72��6>   $��23,0,0,0q�6J   $��5��6\   $	��Top=�6i   $��76��6�   $��2��6�   .+�5=      	� -   5        $��	YesGarden��5=      $
��כן��6+   $��0,0,10,0q�68   	� 5>      -   5        $��NoGarden��5>      $
��לא��6*   $��0,0,10,0q�67   	� 5?      -   5        $��PossibleGarden��5?      $��
אפשרי��60   $��24��6@   $
 TrueY�6L   5@      �� 5A   
   $
��Left=�6   $ Center=�6/   $��
126,10,0,0q�6F   $��5��6Z   $
��Wrap=�6g   $��חצר��6{   $	��Top=�6�   $��69��6�   $��37��6�   $
��Bold"�6�   $��18(�6�   ���	#FF2814A46�   �� 5B   
   -   5        $��Attractions��5B      $
��Left=�6(   $��72��6C   $��	220,0,0,0q�6O   $��5��6b   $	��Top=�6o   $��76��6�   $��2��6�   .+�5C      	� -   5        $��YesAttractions��5C      $
��כן��60   $��0,0,10,0q�6=   	� 5D      -   5        $��NoAttractions��5D      $
��לא��6/   $��0,0,10,0q�6<   	� 5E      -   5        $��PossibleAttractions��5E      $��
אפשרי��65   $��26��6E   $
 TrueY�6Q   5F      �� 5G   
   $
��Left=�6   $ Center=�6/   $��
296,10,0,0q�6F   $��5��6Z   $
��Wrap=�6g   $!��קרוב לאטרקציות��6{   $	��Top=�6�   $	��123��6�   $��47��6�   $
��Bold"�6�   $��18(�6�   ���	#FF2814A46�   �� 5H   
   -   5        $��OkButton��5H      $��
אישור��6!   $
��Left=�61   $��48,5,0,0q�6L   $��7��6^   $	��Top=�6k   $	��110��6�   $��25��6�   ���	#FF1150BF$
��Bold"�6�   6�   �� 5I   
   -   5        $��CancelButton��5I      $��
ביטול��6%   $
��Left=�65   $��	198,5,0,0q�6P   $��7��6c   $	��Top=�6p   $	��110��6�   $��25��6�   $
��Bold"�6�   ���	#FFFF32056�   �� 5L   
   -   5        	 ��Name$	 cbArea��5L      $��1��6#   $
��Left=�63   $��	121,0,0,0q�6N   $��5��6a   $��Bottom=�6n   $	��141��6�   ��5N      �� 5O      .R�5P      �� ��6    �� 
 �� 	Converter#
 [  5Q      5S      �� 5V   
   -   5        $��cbVacationType��5V      $��1��6)   $
��Left=�69   $��	121,0,0,0q�6T   $��6��6g   $��Bottom=�6t   $	��141��6�   ��5W      �� 5X      .R�5Y      �� ��6    �� #
 [  5Z      5\      �� 5]   
   $
��Left=�6   $ Center=�6/   $��כתובת מייל��6F   $��	267,0,0,0q�6X   $��4��6k   $
��Wrap=�6x   $��Bottom=�6�   $	��142��6�   $��31��6�   $��1��6�   $
��Bold"�6�   $��20(�6�   ���	#FF792C8F6�   �� 5^   
   $
��Left=�6   $ Center=�6/   $��שם משפחה��6F   $��	267,0,0,0q�6V   $��3��6i   $
��Wrap=�6v   $��Bottom=�6�   $	��142��6�   $��31��6�   $��1��6�   $
��Bold"�6�   $��20(�6�   ���	#FF792C8F6�   �� 5_   
   $
��Left=�6   $ Center=�6/   $��שם פרטי��6F   $��	267,0,0,0q�6U   $��2��6h   $
��Wrap=�6u   $��Bottom=�6�   $	��142��6�   $��31��6�   $��1��6�   $
��Bold"�6�   $��20(�6�   ���	#FF792C8F6�   �� 5`   
   -   5        $��tbPrivateName��5`      $��1��6'   $
��Left=�67   $��31��6R   $��	121,0,0,0q�6^   $��2��6q   $
 Wrap=�6~   $��Bottom=�6�   $	��146��6�   �� 5a   
   -   5        $��tbFamilyName��5a      $��1��6&   $
��Left=�66   $��31��6Q   $��	121,0,0,0q�6]   $��3��6p   $
 Wrap=�6}   $��Bottom=�6�   $	��146��6�   �� 5b   
   -   5        $��tbMailAddress��5b      $��1��6'   $
��Left=�67   $��31��6R   $��	121,0,0,0q�6^   $��4��6q   $
 Wrap=�6~   $��Bottom=�6�   $	��146��6�   �� 5c   
   -   5        $��ErrorEmptyPrivateName��5c      $y�Hidden=�61   $��1��6E   $ Center=�6U   $
��Left=�6l   $��	121,0,0,0q�6�   $��2��6�   $
��Wrap=�6�   $��שדה חובה��6�   $	��Top=�6�   $��20��6�   $	��141��6�   ���	#FFDC0F0F6�   �� 5d   
   -   5        $��ErrorEmptyFamilyName��5d      $y�Hidden=�60   $��1��6D   $ Center=�6T   $
��Left=�6k   $��	121,0,0,0q�6�   $��3��6�   $
��Wrap=�6�   $��שדה חובה��6�   $	��Top=�6�   $��20��6�   $	��141��6�   ���	#FFDC0F0F6�   �� 5e   
   -   5        $��ErrorEmptyMail��5e      $y�Hidden=�6*   $��1��6>   $ Center=�6N   $
��Left=�6e   $��	121,0,0,0q�6�   $��4��6�   $
��Wrap=�6�   $��שדה חובה��6�   $	��Top=�6�   $��20��6�   $	��141��6�   ���	#FFDC0F0F6�   �� 5f   
   -   5        $��ErrorEmptyArea��5f      $y�Hidden=�6*   $��1��6>   $ Center=�6N   $
��Left=�6e   $��	121,0,0,0q�6�   $��5��6�   $
��Wrap=�6�   $��שדה חובה��6�   $	��Top=�6�   $��20��6�   $	��141��6�   ���	#FFDC0F0F6�   �� 5g   
   -    5        $��ErrorEmptyType��5g      $y�Hidden=�6*   $��1��6>   $ Center=�6N   $
��Left=�6e   $��	121,0,0,0q�6�   $��6��6�   $
��Wrap=�6�   $��שדה חובה��6�   $	��Top=�6�   $��20��6�   $	��141��6�   ���	#FFDC0F0F6�   �� 5h   
   -!   5        $��ErrorEmptyChildrens��5h      $y�Hidden=�6/   $��0��6C   $
 Left=�6S   $
��Left=�6h   $��23,0,0,0q�6�   $��2��6�   $
��Wrap=�6�   $��שדה חובה��6�   $	��Top=�6�   $��20��6�   $	��109��6�   ���	#FFDC0F0F6�   �� 5i   
   -"   5        $��ErrorEmptyAdults��5i      $y�Hidden=�6,   $��0��6@   $
 Left=�6P   $
��Left=�6e   $��	258,0,0,0q�6�   $��2��6�   $
��Wrap=�6�   $��שדה חובה��6�   $	��Top=�6�   $��20��6�   $	��136��6�   ���	#FFDC0F0F6�   �� 5j   
   -#   5        $��ErrorEmptyEntryDate��5j      $y�Hidden=�6/   $��1��6C   $
 Left=�6S   $
��Left=�6h   $��32,0,0,0q�6�   $��1��6�   $
��Wrap=�6�   $��שדה חובה��6�   $	��Top=�6�   $��20��6�   $	��100��6�   ���	#FFDC0F0F6�   �� 5k   
   -$   5        $��ErrorEmptyReleasDate��5k      $y�Hidden=�60   $��0��6D   $
 Left=�6T   $
��Left=�6i   $��23,0,0,0q�6�   $��1��6�   $
��Wrap=�6�   $��שדה חובה��6�   $	��Top=�6�   $��20��6�   $��98��6�   ���	#FFDC0F0F6�   �� 5l   
   -%   5        $��ErrorContentMail��5l      $y�Hidden=�6,   $��1��6@   $ Center=�6P   $
��Left=�6g   $��95,0,0,0q�6�   $��4��6�   $
��Wrap=�6�   $-��'הכנס כתובת מייל תקינה��6�   $	��Top=�6�   $	��172��6�   $��25��6�   ���	#FFDC0F0F6  5m      5n      