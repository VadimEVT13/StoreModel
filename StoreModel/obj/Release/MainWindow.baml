   M S B A M L   `   `   `  ���� E  AStoreModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:StoreModel
StoreModel  R NOxyPlot.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0>.clr-namespace:OxyPlot.Wpf;assembly=OxyPlot.WpfOxyPlot.Wpf     StoreModel.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:StoreModel     >oxy.clr-namespace:OxyPlot.Wpf;assembly=OxyPlot.Wpf       � Title$  Приложение��$	��375��$	��525��.���  � ColumnDefinitions �� $	��200 ��� $��* �.+�� $��0��
�������.+��� .+��� .��Коэф. k:�� -    ��Name$ K_input��$��5,5,5,5q��� .��Дней в периоде:�� -   $ N_input��$��5,5,5,5q��� .��&$Стоимость хранения:�� -   $ Ch_input��$��5,5,5,5q��� .��(&Стоимость перевозок:�� -   $ C0_input��$��5,5,5,5q��� -    ��Name$ Col_dostav_label��.��(&Количество доставок:�� -   $��5,5,5,5q�.��Выполнить� $��5,5,5,5q� �� $��60 ��� $��* �.+��� $��0��.��
Дата�� -   $ 
Date_input��$��1���� -   $��5,5,5,5q�.��!Сохранить данные� $��1��.+�  OxyPlot.Wpf.Plot     Axes   OxyPlot.Wpf.LinearAxis    OxyPlot.Wpf.Axis   Position$ Bottom=�   Title$ Номер дня��  $
 Left=�$' !Количество товара��   Series.   OxyPlot.Wpf.LineSeries  -	     Name$ LineSeries1��  OxyPlot.Wpf.Series	   Title$	 
Спрос��!  OxyPlot.Wpf.DataPointSeries
   
DataFieldX$
 X��   
DataFieldY$ Y��  -
   $ LineSeries2��$$	 Товара на складе��$
 X��$ Y��  -   $ LineSeries3��$<	 6Товара на складе с поставками��$
 X��$ Y��