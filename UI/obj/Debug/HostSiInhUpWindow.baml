   M S B A M L   `   `   `  ����=  9UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:UIUI  5   	       UI.HostSiInhUpWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   "localclr-namespace:UI     5   	     � Title$  HostSiInhUpWindow��5   	   $	��500��6#   $	��850��60    � WindowStartupLocation$ CenterScreen=�6<   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��1.5* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   ��  �� TextAlignment$ Center=�6   $
��Left=�6+   $
��Wrap=�6F   $����מארח יקר! אנו שמחים שבחרת להשתמש במערכת שלנו, בבקשה מלא את הפרטים הבאים��6Z   $��Bottom=�6�   $	��792��6�   $��64��6�   $��2��6�   $��22(�6�   $
��Bold"�6�   $��
Wide Latin)�6  ���	#FF8364646(  �� 5   
   $��1��6   $
��Left=�6$   $ Right=�6?   $��	259,0,0,0q�6U   $��1��6h   $
��Wrap=�6u   $��:שם פרטי��6�   $��Bottom=�6�   $	��115��6�   $��22��6�   $
��Bold"�6�   $��14(�6�   ���	#FF40148D6�   �� 5   
   -   5        $��tbPrivateName��5      $��1��6'   $
��Left=�67   $��23��6R   $��
106,27,0,0q�6^   $��1��6r   $��2��6    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��148��6�   �� 5   
   $��1��6   $
��Left=�6$   $ Right=�6?   $��	259,0,0,0q�6U   $��2��6h   $
��Wrap=�6u   $��:שם משפחה��6�   $��Bottom=�6�   $	��115��6�   $��22��6�   $
��Bold"�6�   $��14(�6�   ���	#FF40148D6�   �� 5   
   -   5        $��tbFamilyeName��5      $��1��6'   $
��Left=�67   $��23��6R   $��
106,27,0,0q�6^   $��2��6r   $��2��6   $
 Wrap=�6�   $	��Top=�6�   $	��148��6�   �� 5   
   $��1��6   $
��Left=�6$   $ Right=�6?   $��	254,0,0,0q�6U   $��3��6h   $
��Wrap=�6u   $#��:מספר תעודת זהות��6�   $��Bottom=�6�   $	��120��6�   $��22��6�   $
��Bold"�6�   $��14(�6�   ���	#FF40148D6�   �� 5   
   -   5        $
��tbID��5       �� 	MaxLength$ 9��6   $��1��6,   $
��Left=�6<   $��23��6W   $��
106,27,0,0q�6c   $��3��6w   $��2��6�   $
 Wrap=�6�   $	��Top=�6�   $	��148��6�   �� 5   
   $��1��6   $
��Left=�6$   $ Right=�6?   $��	259,0,0,0q�6U   $��4��6h   $
��Wrap=�6u   $��:בחר ססמא��6�   $��Bottom=�6�   $	��115��6�   $��22��6�   $
��Bold"�6�   $��14(�6�   ���	#FF40148D6�   G� 5    
   -   5        $��
pbPassword��5       $��1��6(    G� 	MaxLength$ 10��68   $
��Left=�6G   $��
106,17,0,0q�6b   $��4��6v   $��Bottom=�6�   $	��148��6�   $��23��6�    J� RenderTransformOrigin$ 
0.5,-0.909&�6�   �� 5!   
   $��1��6   $
��Left=�6$   $ Right=�6?   $��	259,0,0,0q�6U   $��5��6h   $
��Wrap=�6u   $��:מספר פלאפון��6�   $��Bottom=�6�   $	��115��6�   $��22��6�   $
��Bold"�6�   $��14(�6�   ���	#FF40148D6�   �� 5"   
   -   5        $��tbFhoneNumber��5"      $ 10��6'   $��1��66   $
��Left=�6F   $��23��6a   $��
106,27,0,0q�6m   $��5��6�   $��2��6�   $
 Wrap=�6�   $	��Top=�6�   $	��148��6�   �� 5#   
   $��1��6   $
��Left=�6$   $ Right=�6?   $��	259,0,0,0q�6U   $��6��6h   $
��Wrap=�6u   $��:אימייל��6�   $��Bottom=�6�   $	��115��6�   $��22��6�   $
��Bold"�6�   $��14(�6�   ���	#FF40148D6�   �� 5$   
   -   5        $��tbMailAddress��5$      $��1��6'   $
��Left=�67   $��23��6R   $��
106,27,0,0q�6^   $��5��6r   $��2��6   $
 Wrap=�6�   $��Bottom=�6�   $	��148��6�   �� 5&   
   $��0��6   $
��Left=�6$   $ Right=�6?   $��	259,0,0,0q�6U   $��1��6h   $
��Wrap=�6u   $��:בחר בנק��6�   $��Bottom=�6�   $	��115��6�   $��22��6�   $
��Bold"�6�   $��14(�6�   ���	#FF40148D6�   �� 5'   
   -   5        $��cbBanks��5'      	 �� 
IsEditable	 . $
��Left=�64   $��72,0,0,0q�6O   $��1��6a   $��Bottom=�6n   $	��182��6�   6�   �� 5(   
   $��0��6   $
��Left=�6$   $ Right=�6?   $��	259,0,0,0q�6U   $��4��6h   $
��Wrap=�6u   $!��:מספר חשבון בנק��6�   $��Bottom=�6�   $	��115��6�   $��22��6�   $
��Bold"�6�   $��14(�6�   ���	#FF40148D6�   �� 5)   
   -   5        $��tbBankAccountNumber��5)      $��0��6-   $
��Left=�6=   $��22��6X   $��	72,27,0,0q�6d   $��4��6w   $
 Wrap=�6�   $	��Top=�6�   $	��182��6�   �� 5*   
   -	   5        $��IsCollectionClearance��5*      $+��%מאשר הרשאת חשבון בנק��60   $
��Left=�6P   $��
193,17,0,0q�6k   $��5��6   $	��Top=�6�   $��23��6�   $	��290��6�   
����������	#FF3F1081$��14(�6�   $
��Bold"�6�   $��2��6  �� 5+   
   -
   5        $��OkButton��5+      $��הרשם למערכת��6!   $
��Left=�67   $��46,0,0,0q�6R   $��6��6d   $��Bottom=�6q   $	��126��6�   $��27��6�   ���	#FF88E5F3���	#FF0A0A09$��16(�6�   $
��Bold"�6�   6�   �� 5-   
   -   5        $��EmailErrorMessege��5-      $y�Hidden=�6-   $��1��6A   $
��Left=�6Q   $��	10,10,0,0q�6l   $��6��6   $ Center=�6�   $
��Wrap=�6�   $)��#כתובת מייל לא תקינה��6�   $	��Top=�6�   $��40��6�   $��78��6�   ���	#FFE809096  �� 5.   
   $��1��6   $y�Hidden=�6$   $
��Left=�68   $ Center=�6S   $��	10,10,0,0q�6j   $��5��6}   $
��Wrap=�6�   $'��!מספר טלפון לא תקין��6�   $	��Top=�6�   $��77��6�   $��40��6�   $��2��6�   ���	#FFE809096�   �� 5/   
   $��0��6   $
��Left=�6$   $ Right=�6?   $��	259,0,0,0q�6U   $��2��6h   $
��Wrap=�6u   $��:בחר סניף בנק��6�   $��Bottom=�6�   $	��115��6�   $��22��6�   $
��Bold"�6�   $��14(�6�   ���	#FF40148D6�   �� 51   
   -   5        $��cbBankBranches��51      {�.  	 . $
��Left=�6M   $��72,0,0,0q�6h   $��2��6z   $��Bottom=�6�   $	��182��6�   �� 52   
   -   5        $��ErrorEmptyPrivateName��52      $y�Hidden=�61   $��1��6E   $
��Left=�6U   $��	10,27,0,0q�6p   $ Right=�6�   $��1��6�   $
��Wrap=�6�   $��שדה חובה��6�   $��Bottom=�6�   $��91��6�   ���	#FFFB0505$��22��6  �� 53   
   -   5        $��ErrorEmptyFamilyName��53      $y�Hidden=�60   $��1��6D   $
��Left=�6T   $��	10,27,0,0q�6o   $ Right=�6�   $��2��6�   $
��Wrap=�6�   $��שדה חובה��6�   $��Bottom=�6�   $��91��6�   ���	#FFFB0505$��22��6  �� 54   
   -   5        $��ErrorEmptyId��54      $y�Hidden=�6(   $��1��6<   $
��Left=�6L   $��	10,27,0,0q�6g   $ Right=�6z   $��3��6�   $
��Wrap=�6�   $��שדה חובה��6�   $��Bottom=�6�   $��91��6�   ���	#FFFB0505$��22��6�   �� 55   
   -   5        $��ErrorEmptyPassword��55      $y�Hidden=�6.   $��1��6B   $
��Left=�6R   $��	10,27,0,0q�6m   $ Right=�6�   $��4��6�   $
��Wrap=�6�   $��שדה חובה��6�   $��Bottom=�6�   $��91��6�   ���	#FFFB0505$��22��6  �� 56   
   -   5        $��ErrorEmptyPhoneNumber��56      $y�Hidden=�61   $��1��6E   $
��Left=�6U   $��	10,27,0,0q�6p   $ Right=�6�   $��5��6�   $
��Wrap=�6�   $��שדה חובה��6�   $��Bottom=�6�   $��91��6�   ���	#FFFB0505$��22��6  �� 57   
   -   5        $��ErrorEmptyEmail��57      $y�Hidden=�6+   $��1��6?   $
��Left=�6O   $��	10,27,0,0q�6j   $ Right=�6}   $��6��6�   $
��Wrap=�6�   $��שדה חובה��6�   $��Bottom=�6�   $��91��6�   ���	#FFFB0505$��22��6  �� 58   
   -   5        $��ErrorEmptyBankName��58      $y�Hidden=�6.   $��0��6B   $
��Left=�6R   $��	10,27,0,0q�6m   $ Right=�6�   $��1��6�   $
��Wrap=�6�   $��שדה חובה��6�   $��Bottom=�6�   $��91��6�   ���	#FFFB0505$��22��6  �� 59   
   -   5        $��ErrorEmptyBankBranch��59      $y�Hidden=�60   $��0��6D   $
��Left=�6T   $��	10,27,0,0q�6o   $ Right=�6�   $��2��6�   $
��Wrap=�6�   $��שדה חובה��6�   $��Bottom=�6�   $��91��6�   ���	#FFFB0505$��22��6  �� 5:   
   -   5        $��ErrorEmptyAccountNumber��5:      $y�Hidden=�63   $��0��6G   $
��Left=�6W   $��	10,27,0,0q�6r   $ Right=�6�   $��3��6�   $
��Wrap=�6�   $��שדה חובה��6�   $��Bottom=�6�   $��91��6�   ���	#FFFB0505$��22��6	  �� 5;   
   -   5        $��ErrorAccountNumber��5;      $y�Hidden=�6.   $��0��6B   $
��Left=�6R   $��	10,27,0,0q�6m   $ Right=�6�   $��3��6�   $
��Wrap=�6�   $��תוכן לא תקין��6�   $��Bottom=�6�   $��91��6�   ���	#FFFB0505$��22��6  �� 5<   
   -   5        $��Errorid��5<      $y�Hidden=�6#   $��1��67   $
��Left=�6G   $��	10,27,0,0q�6b   $ Right=�6u   $��3��6�   $
��Wrap=�6�   $��תוכן לא תקין��6�   $��Bottom=�6�   $��91��6�   ���	#FFFB0505$��22��6�   �� 5=   
   -   5        $��ErrorPhoneNumber��5=      $y�Hidden=�6,   $��1��6@   $
��Left=�6P   $��	10,27,0,0q�6k   $ Right=�6~   $��5��6�   $
��Wrap=�6�   $��תוכן לא תקין��6�   $��Bottom=�6�   $��91��6�   ���	#FFFB0505$��22��6  �� 5>   
   -   5        $��
ErrorEmail��5>      $y�Hidden=�6&   $��1��6:   $
��Left=�6J   $��	10,27,0,0q�6e   $ Right=�6x   $��6��6�   $
��Wrap=�6�   $��תוכן לא תקין��6�   $��Bottom=�6�   $��91��6�   ���	#FFFB0505$��22��6   5@      5A      