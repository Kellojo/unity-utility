using System.Collections;
using System.Collections.Generic;
using Kellojo.Items;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Kellojo.UI {

    public class ItemTile : MonoBehaviour {

        [SerializeField] TMP_Text NameText;
        [SerializeField] TMP_Text AmountText;
        [SerializeField] Image Icon;

        public void SetItem(Item item, int amount) {
            if (item != null) {
                NameText.SetText(item.name);
                AmountText.SetText(amount.ToString());
                Icon.sprite = item.Icon;
            }
        }


    }

}

