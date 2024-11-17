# Mtf.Maui.Controls Documentation

## Overview
The `Mtf.Maui.Controls` library provides a set of reusable, customizable `ContentView` components for .NET MAUI applications. These components enhance user interface capabilities with features like labeled controls, hyperlinks, and numeric input.

---

## Components

### 1. `CheckBoxWithLabel`
A `ContentView` combining a `CheckBox` with a customizable label.

#### Properties
- **Label** (`string`): The text displayed alongside the checkbox.
- **IsChecked** (`bool`): The checked state of the checkbox. Supports two-way binding.

#### Commands
- **ToggleCommand**: Toggles the `IsChecked` state programmatically.

#### Example Usage
```xml
<controls:CheckBoxWithLabel 
    Label="Accept Terms and Conditions" 
    IsChecked="{Binding IsAccepted}" />
```

---

### 2. `EntryWithLabel`
A labeled `Entry` field with extensive customization options.

#### Properties
- **Label** (`string`): The label displayed above or alongside the entry.
- **Placeholder** (`string`): Placeholder text for the entry. Defaults to `Label` if not set.
- **Text** (`string`): The entry’s text. Supports two-way binding.
- **Keyboard** (`Keyboard`): The type of keyboard (e.g., `Keyboard.Numeric`).
- **IsPassword** (`bool`): Indicates if the entry should mask text (e.g., for passwords).
- **IsReadOnly** (`bool`): Disables text input when `true`.
- **EntryTextColor** (`Color`): Color of the entry text.
- **EntryMinimumWidthRequest** (`int`): Minimum width of the entry.
- **EntryMinimumHeightRequest** (`int`): Minimum height of the entry.

#### Events
- **TextChanged**: Triggered when the `Text` value changes.

#### Commands
- **CopyToClipboardCommand**: Copies the entry’s text to the clipboard.

#### Example Usage
```xml
<controls:EntryWithLabel 
    Label="Username" 
    Placeholder="Enter your username" 
    Text="{Binding Username}" 
    IsPassword="False" />
```

---

### 3. `Hyperlink`
A clickable hyperlink styled with visual feedback.

#### Properties
- **Url** (`string`): The URL to navigate to.
- **LinkLabel** (`string`): The text displayed for the hyperlink.

#### Behavior
- Changes color on hover.
- Opens the URL in the system’s default browser.

#### Example Usage
```xml
<controls:Hyperlink 
    Url="https://example.com" 
    LinkLabel="Visit Example" />
```

---

### 4. `MenuItemView`
A customizable menu item with navigation support.

#### Properties
- **ImageSource** (`List<string>`): Image sources for the menu icon.
- **LabelText** (`string`): Text displayed on the menu item.
- **PageType** (`Type`): The page to navigate to when clicked.
- **Parameter** (`object`): Parameter to pass during navigation.
- **AfterExecution** (`ICommand`): Command to execute after navigation.

#### Commands
- **NavigateCommand**: Navigates to the specified page.

#### Example Usage
```xml
<controls:MenuItemView 
    LabelText="Settings" 
    PageType="{x:Type pages:SettingsPage}" />
```

You can set up the `MenuItemView` to download images from an online source.
```csharp
ImageSettings.UseOfflineImages = false;
ImageSettings.ImagesUrl = "https://cdn.example.com/images/";
ImageSettings.NumberOfDaysToCacheImages = 30; // Cache 30 napig
```

---

### 5. `NumericUpDownWithLabel`
A numeric input control with increment and decrement buttons.

#### Properties
- **Label** (`string`): Label displayed next to the control.
- **Value** (`double`): Current value. Supports two-way binding.
- **Minimum** (`double`): Minimum allowed value.
- **Maximum** (`double`): Maximum allowed value.
- **Increment** (`double`): Step size for value changes.

#### Events
- **ValueChanged**: Triggered when `Value` changes.

#### Behavior
- Pressing and holding increment or decrement buttons changes the value repeatedly.

#### Example Usage
```xml
<controls:NumericUpDownWithLabel 
    Label="Quantity" 
    Value="{Binding Quantity}" 
    Minimum="1" 
    Maximum="100" 
    Increment="1" />
```

---

## Dependencies
- **CommunityToolkit.Mvvm**: Used for commands and messaging.
- **System.Maui**: Base dependency for MAUI applications.

---

## Installation
Add the `Mtf.Maui.Controls` library to your MAUI project via NuGet:
```bash
dotnet add package Mtf.Maui.Controls
```

---

## License
This library is provided under the MIT License. Feel free to use and modify it in your applications.
