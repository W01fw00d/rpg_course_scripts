﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : BaseItem {

    [SerializeField]
    private ItemCategory category;

    [SerializeField]
    private float strength;

    [SerializeField]
    private float weight;

    public ItemCategory Category
    {
        get
        {
            return category;
        }

        set
        {
            category = value;
        }
    }

    public float Strength
    {
        get
        {
            return strength;
        }

        set
        {
            strength = value;
        }
    }

    public float Weight
    {
        get
        {
            return weight;
        }

        set
        {
            weight = value;
        }
    }
}
