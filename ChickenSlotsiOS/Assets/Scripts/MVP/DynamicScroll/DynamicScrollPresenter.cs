using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicScrollPresenter
{
    private DynamicScrollModel dynamicScrollModel;
    private DynamicScrollView dynamicScrollView;

    public DynamicScrollPresenter(DynamicScrollModel dynamicScrollModel, DynamicScrollView dynamicScrollView)
    {
        this.dynamicScrollModel = dynamicScrollModel;
        this.dynamicScrollView = dynamicScrollView;
    }

    public void Initialize()
    {
        ActivateEvents();

        dynamicScrollView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        dynamicScrollView.Dispose();
    }

    private void ActivateEvents()
    {
        dynamicScrollView.OnSelectGameIcon += dynamicScrollModel.Select;

        dynamicScrollModel.OnSelectGameIcon += dynamicScrollView.SmoothScroll;
    }

    private void DeactivateEvents()
    {
        dynamicScrollView.OnSelectGameIcon -= dynamicScrollModel.Select;

        dynamicScrollModel.OnSelectGameIcon -= dynamicScrollView.SmoothScroll;
    }
}
