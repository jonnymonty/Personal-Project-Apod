   M S B A M L   `   `   `  ����?  ;Apod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ApodApod  5   	       Apod.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Apod     5   	   -   5          � Title$"  Astronomy Picture of the Day��5   	   $	��565��6.   $	��700��6;    � 
ResizeMode$ CanMinimize=�5	   	    � WindowStartupLocation$ CenterScreen=�5   	    � Icon$ Images/moon.ico��5   	   .��5      � .+�5   
   �� $��Images/SpaceBG.jpg��6   $��Center=�5      $��UniformToFill=�5      �� 5   
   $��
15 15 15 0q�6   .+�5      � $��0 0 0 5q�6   .+�5      �� -   5         ��Name$ 	ImageName��5      
�������$��Center=�5      $
��Left=�5      $
��Wrap=�5      $��22(�5      ��5      �� *Context.Title+5      �� 5       �� Orientation$ 
Horizontal=�6   $��Center=�66   $��Right=�6Q   .+�5      (  "System.Windows.Controls.DatePicker  -   5          Name$ 
DatePicker��5   !   $��0 6 0 0q�5   !   $��0q�5   !   $��30��5    !   $	��115��5!   !   $��0q�5"   !   6,   5$      5%      �� 5'      -   5         ��Name$ 	ApodImage��5'      $��Center=�5)      $��Center=�5*      $	��400��5+      $��0q�5,      $��UniformToFill=�5-      ��5(      �� *Context.ImageSource+5-      50      �� 51   
   $��
15 0 15 15q�6   $��Bottom=�52      $ 
Horizontal=�53      $
��Left=�54      .+�56      �� -   5         ��Name$ 
SaveButton��56      $
��Save��57      $��0 0 15 0q�58      $��2q�59         FontSizeRegular#��[  5:      $��75��5;      $��30��5<      6!   �� 5>      -   5        $ SetBackground��5>      $��Set Background��5?      $��2q�5@      #��[  5A      {�.  $��0 0 15 0q�5C      $	��115��5D      $��30��5E      6!   �� 5G      -   5        $ Status��5G      
�������$��15(�5J      $
��Wrap=�5K      $��Center=�5L      ��5H      �� *Context.Status+5L      5M      5N      5O      