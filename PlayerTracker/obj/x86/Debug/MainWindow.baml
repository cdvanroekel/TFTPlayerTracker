   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	    PlayerTracker   PlayerTracker.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Kmah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          � Title$  Player Tracker��5   	   $	��300��6    $	��275��6-    � Topmost .  � WindowStyle$ SingleBorderWindow=�6a   .��5      �  � ColumnDefinitions 5	   
   �� 5
      $��* �6   �� 5      $��5 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � -   5        $��prgLevel��5      $��0��6&   $��0��64   $��3��6D   �� 5   
   -   5        $��
scvPlayers��5      $��1��6)   $��0��66   .��5      �� -   5        $��
txtPlayers��5      $��12(�6(   
���  �� �� AcceptsReturn . $��0q�6\   5      �� 5   
   $��5��6   $��1��6!   $��1��6.   �� 5   
   -   5        $��	scvSpoken��5      $��1��6(   $��2��65   .��5      �� -   5        $��	txtSpoken��5      $��12(�6)   
�������67   5      �� 5   
   -   5         ��Name$ 	btnUpdate��5      $��2��6$   $��3��61   .��6`   Update Player List6t   5      5      