using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PortraitFinder.App.ViewModels;

public abstract partial class FlagEnumViewModelBase(string name, bool isExpanded = false) : ObservableObject
{
    public string Name { get; } = name;

    public abstract IEnumerable Options { get; }

    [ObservableProperty]
    private bool isExpanded = isExpanded;
}
