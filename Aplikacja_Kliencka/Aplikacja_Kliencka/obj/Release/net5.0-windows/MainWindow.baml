   M S B A M L   `   `   `  ���� M  IAplikacja_Kliencka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Aplikacja_KlienckaAplikacja_Kliencka  #    Aplikacja_Kliencka.MainWindow   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:Aplikacja_Kliencka     Y UFontAwesome.Sharp, Version=5.15.4.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  -     � Title$3  -Zarzadzanie rozproszonymi obliczeniami-Klient��$	��680��$
��1200��$	��670��$
��1200��.��� .+�� $
��Left=�$	��300�� J� RenderTransformOrigin$ 0,0.5&����	#FF1F1E44.+�� $
��left=�$	��Top=�$	��140��$��0,0,0,0q�$	��300��.+��� -    ��Name$ btn_home����Z� ��Z� ��Z� ��!  FontAwesome.Sharp.IconBlock  �  �FontAwesome.Sharp.IconBlockBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=5.15.4.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ Tasks=�
���i��$��72(��� $��Hidden=�$��	0,140,0,0q�.��� -    �Name$ 	grid_menu��$��0,0,0,0q�.+��� -   $ btn_dashBoard��$��60��$��0,0,0,0q�$	��Top=�
���i��$��36(���Z� ��Z� ����  �� Orientation$ 
Horizontal=�$	��300��$��60��.+�  $ TachometerAlt=�$��36(�$��20,0,0,0q�$��Center=�$��Center=��� $��PULPIT��$��20,0,0,0q�$��Center=�$��Center=��� -   $ btn_view��$��60��$��0,70,0,0q�$	��Top=�
���i��$��36(���Z� ��Z� ���� $ 
Horizontal=�$	��300��$��60��.+�  $	 Eye=�$��36(�$��20,0,0,0q�$��Center=�$��Center=��� $��PODGLAD��$��20,0,0,0q�$��Center=�$��Center=�� -   $ 
grid_views��$	��300��$��	0,140,0,0q�$��60��$	��Top=�$
��Left=��� -   $ btn_settings��$��60��$��	0,140,0,0q�$	��Top=�
���i��$��36(�$y�Visible=���Z� ��Z� ���� $ 
Horizontal=�$	��300��$��60��.+�  $	 Cog=�$��36(�$��20,0,0,0q�$��Center=�$��Center=��� $��
USTAWIENIA��$��20,0,0,0q�$��Center=�$��Center=��� -   $ btn_settings_con��$��60��$��
10,350,0,0q�$	��Top=�
�������$��36(�$y�	Collapsed=���Z� ��Z� ���� $ 
Horizontal=�$	��290��$��60��.+�  $
 Link=�$��36(�$��20,0,0,0q�$��Center=�$��Center=��� $��POŁĄCZENIE��$��20,0,0,0q�$��Center=�$��Center=��� -	   $ btn_settings_que��$��60��$��
10,410,0,0q�$	��Top=�
�������$��36(�$y�	Collapsed=���Z� ��Z� ���� $ 
Horizontal=�$	��290��$��60��.+�  $
 Cube=�$��36(�$��20,0,0,0q�$��Center=�$��Center=��� $��KOLEJKI��$��20,0,0,0q�$��Center=�$��Center=��� -
   $ btn_settings_cli��$��60��$��
10,470,0,0q�$	��Top=�
�������$��36(�$y�	Collapsed=���Z� ��Z� ���� $ 
Horizontal=�$	��290��$��60��.+�  $
 User=�$��36(�$��20,0,0,0q�$��Center=�$��Center=��� $��KLIENCI��$��20,0,0,0q�$��Center=�$��Center=��� -   $ btn_settings_task��$��60��$��
10,530,0,0q�$	��Top=�
�������$��36(�$y�	Collapsed=���Z� ��Z� ���� $ 
Horizontal=�$	��290��$��60��.+�  $ 
PaperPlane=�$��36(�$��20,0,0,0q�$��Center=�$��Center=��� $��KOMENDY��$��20,0,0,0q�$��Center=�$��Center=�� $��	300,0,0,0q�$��80��$	��Top=����	#FF1B1A38.+�  -     Name$ ico_heder_ico��$
 Home=�
���i��$��58(�$
��Left=�$��20,0,0,0q��� -    ��Name$ lbl_heder_label��$�� ��$
��Left=�$��	100,0,0,0q�$��Center=�$��48(�$	��840��
�������� -   $ 
grid_board��$��
300,80,0,0q����	#FF313066