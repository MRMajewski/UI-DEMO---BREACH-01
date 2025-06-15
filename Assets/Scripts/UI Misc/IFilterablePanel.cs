using System;
using System.Collections.Generic;

public interface IFilterablePanel<T> 
{
    void FilterUpdated(HashSet<T> selectedCategories);
}