   M S B A M L   `   `   `  ����C  ?MainPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MainPageMainPage  5          MainPage.RegisterPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:MainPage     5        N� Title$  RegisterPage��5	      .��5      � .+�5   
   �� $��Regisztráció��6   $��Center=�6'   $��25(�6D   $��0,0,0,10q�6R   
�������6d   �� 5   
   $��Felhasználónév:��6   $
��Left=�6*   $	��Top=�6E   $��14(�6]   $��
10,100,0,0q�6k   
�������6   �� 5   
   -   5         ��Name$ txtName��5      $��Center=�6#   $��	0,100,0,0q�6@   $	��Top=�6S   $	��330��6k   $��25��6w    �� TextWrapping$
 Wrap=�6�   6�   �� 5   
   $��Email cím:��6   $
��Left=�6%   $	��Top=�6@   $��14(�6X   $��10,150,0,105q�6f   $��79��6|   
�������6�   �� 5   
   -   5        $ txtEmail��5      $��Center=�6$   $��	0,150,0,0q�6A   $	��Top=�6T   $	��330��6l   $��25��6x   $
 Wrap=�6�   �� 5   
   $��Jelszó:��6   $
��Left=�6"   $��
10,200,0,0q�6=   $	��Top=�6Q   $��94��6i   $��26��6t   $��14(�6�   
�������6�   �� 5   
   -   5        $ txtPassword��5      $��Center=�6'   $
 Wrap=�6D   $	��Top=�6X   $	��330��6p   $��24��6|   $��	0,200,0,0q�6�   �� 5   
   $��Jelszó megerősítése:��6   $
��Left=�6/   $��
10,250,0,0q�6J   $	��Top=�6^   $	��158��6v   $��14(�6�   
�������6�   �� 5   
   -   5        $ txtPassword2��5      $��Center=�6(   $��	0,250,0,0q�6E   $
 Wrap=�6X   $	��Top=�6l   $	��330��6�   $��25��6�   �� 5   
   -   5         ��Name$ bttnReg��5      $��Regszitráció��6"   $��30��6O   $	��100��6[   $��Bottom=�6g   $��0,0,0,50q�6�   �� 5   
   -   5        $ bttnBack��5      $��Vissza��6#   $��30��6C   $��70��6O   $
��Left=�6Z   $	��Top=�6u   $��10q�6�   �� 5   
   -   5        $ bttnExit��5      $��X��6#   $��20(�6/   $��Right=�6=   $	��Top=�6Y   $��40��6q   $��40��6|   ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value 5      �� 5       �� 
TargetType" ��6.   .R�5   "   �� #��z��6)   ._�5   &   �� $��Center=�67   $��Center=�6T   5   #   5       5"       �� Triggers 5#      P� 5$      	 P� Property	 �@J�IsMouseOver
 P� Value$

 True��69   . �5%      ��  � ��
 �  ��6;   5&      5(      5*      5+      5,      