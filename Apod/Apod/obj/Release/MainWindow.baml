   M S B A M L   `   `   `  ���� ?  ;Apod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ApodApod      Apod.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     $localclr-namespace:Apod     -     � Title$"  Astronomy Picture of the Day��$	��540��$	��700�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=� � Icon$ Images/moon.ico��.��� .+��� $��
15 15 15 0q�.+�� $��0 0 0 5q�.+��� -    ��Name$ 	ImageName��$��Center=�$
��Left=�$
��Wrap=�$��22(����� *Context.Title+��  �� Orientation$ 
Horizontal=�$��Center=�$��Right=�.+�(  "System.Windows.Controls.DatePicker  -     Name$ 
DatePicker��$��0 6 0 0q�$��0q�$��30��$	��115��$��0 0 15 0q��� -    ��Name$ RefreshButton��$��Refresh��$��75��$��30���� -    ��Name$ 	ApodImage��$��Center=�$��Center=�$	��400��$��0q�$��UniformToFill=����� *Context.ImageSource+�� $��
15 0 15 15q�$��Bottom=�$ 
Horizontal=�$
��Left=�.+��� -   $ 
SaveButton��$
��Save��$��0 0 15 0q�$��75��$��30���� -   $ SetBackground��$��Set Background��{�.  $��0 0 15 0q�$	��115��$��30���� -   $ Status��$��15(�$
��Wrap=�$��Center=����� *Context.Status+