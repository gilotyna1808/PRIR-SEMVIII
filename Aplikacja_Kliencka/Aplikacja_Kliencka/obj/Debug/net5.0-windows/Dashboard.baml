   M S B A M L   `   `   `  ����M  IAplikacja_Kliencka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Aplikacja_KlienckaAplikacja_Kliencka  5      "    Aplikacja_Kliencka.Dashboard   5      \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:Aplikacja_Kliencka     5      .��5	      � .+�5
   
   �� -   5          ��Name$  btn_add��5
      $��Dodaj��6"   $
��Left=�62   $��	10,10,0,0q�6M   $	��Top=�6`   $	��100��6x   $	��100��6�   6�   �� 5   
   -   5         ��Name$ lbl_numberOfActiveClients��5      $ ��Liczba aktywnych klientow:��63   $��120,10,120,0q�6X   $	��Top=�6n   $��Center=�6�   $��50��6�   $��20(�6�   
�������6�   �� 5   
   -   5        $" lbl_maxNumberOfActiveClients��5      $+��%Maksymalna liczba aktywnych klientow:��66   $��
0,60,319,0q�6f   $	��Top=�6z   $��Center=�6�   $��50��6�   $��20(�6�   
�������$��Right=�6�   �� 5   
   $��Dodaj��6   $
��Left=�6    $��
10,115,0,0q�6;   $	��Top=�6O   $	��100��6g   $��30��6s   $��Center=�6   
�������6�   �� 5   
   -   5         ��Name$ 	txt_add_x��5      $��Center=�6%   $��Center=�6B   $��-380,115,0,0q�6f   $��1��6|    �� TextWrapping$
 Wrap=�6�   $��30��6�   $��Center=�6�   $	��Top=�6�   $	��125��6�   6�   �� 5   
   -   5        $  	btn_add_x��5      $��Dodaj: 1��6$   $��30��67   $	��125��6C   $��Center=�6O   $��-125,115,0,0q�6l   $	��Top=�6�   6�   �� 5   
   -   5        $  btn_start_all��5      $��Uruchom wszystkich��6(   $��45��6E   $	��125��6Q   $��right=�6]   $��664,10,10,0q�6y   $	��Top=�6�   ��6�   Z� 6�   �� 5   
   -   5        $  btn_stop_all��5      $��Zatrzymaj wszystkich��6'   $��45��6F   $	��125��6R   $��Right=�6^   $��385,60,10,0q�6z   $	��Top=�6�   ��6�   Z� 6�   �� 5   
   $#��Podgląd aktywnych klientów:��6   $��Center=�66   $��Center=�6S   $��Center=�6u   $��50��6�   $��	0,150,0,0q�6�   $	��Top=�6�   $��20(�6�   
�������6�   �� 5   
   $��	0,200,0,0q�6   $
��Auto=�6*   .��5      � -   5         �Name$ grid_activeClientsView��5      $��0,0,0,0q�63   5      5      5      5      