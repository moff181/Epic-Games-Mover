   M S B A M L   `   `   `  ����I  EEpicGamesMover, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:EpicGamesMoverEpicGamesMover  5   	       EpicGamesMover.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:EpicGamesMover     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      ��  �� Orientation$ Vertical=�6   .+�5
   
   �� $9��3Please make sure the epic games launcher is closed.��6   �� 5   
   -   5         ��Name$ Status��5      �� 5   
   -   5         ��Name$ ManifestDirectory��5      $ 
Horizontal=�60   .+�5      �� -   5         ��Name$ ManifestDirectoryLabel��5      $��-2, 0, 0, 0q�64   $
��Bold"�6I   .��6[   Manifest Directory:6p   �� 5      -   5         ��Name$ ManifestDirectoryBox��5   "   $	��500��6   6"   5      �� 5   
   -   5        $ 	NamePanel��5      $ 
Horizontal=�6(   .+�5      �� -   5        $ 	NameLabel��5      $��-2, 0, 0, 0q�6'   $
��Bold"�6<   .��6N   Name:6U   �� 5      -   5        $ NameBox��5   "   $	��500��6   6"   5      �� 5   
   -   5        $ SourcePanel��5      $ 
Horizontal=�6*   .+�5      �� -	   5        $ SourceLabel��5      $��-2, 0, 0, 0q�6)   $
��Bold"�6>   .��6P   Source Directory:6c   �� 5      -
   5        $ 	SourceBox��5   "   $	��500��6   6"   5      �� 5   
   -   5        $ DestinationPanel��5      $ 
Horizontal=�6/   .+�5      �� -   5        $ DestinationLabel��5      $��-2, 0, 0, 0q�6.   $
��Bold"�6C   .��6U   Destination Directory:6m   �� 5      -   5        $ DestinationBox��5   "   $	��500��6   6"   5      �� 5   
   -   5        $	��Run��5      6   5      5      