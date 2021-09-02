using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ziggurat.UI
{
    [RequireComponent(typeof(Collider))]
    public sealed class ClickComponent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Renderer _renderer;
        private Material _defaultMaterial;
        private Material _selectedMaterial;

        [field: SerializeField, RenameField("Selectable")]
        public bool Selectable { set; get; } = true;
        public bool Selected { private set; get; }
        [Space, SerializeField, Range(0, 2)]
        private float _outlineWidth = 0.5f;

        public event Action selected;

        private void Awake()
        {
            try
            {
                _defaultMaterial = _renderer.material;
                _selectedMaterial = Resources.Load("Materials/Selected") as Material;
            }
            catch (Exception e)
            {
                Debug.LogError(name + " has error in ClickComponent!");
                Debug.LogError(e);
            }
            finally
            {
                if (_selectedMaterial == null) Debug.LogWarning("Selected Material was not loaded!");
            }
        }
            
        public void Select(bool isSelected)
        {
            if (_renderer == null || !Selectable) return;

            if (isSelected)
            {
                Color color = _renderer.material.color;
                _renderer.material = _selectedMaterial;
                _renderer.material.SetColor("_Color", color);
                _renderer.material.SetFloat("_OutlineWidth", _outlineWidth);
            }
            else
            {
                _renderer.material = _defaultMaterial;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!Selectable) return;

            selected?.Invoke();
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!Selected) Select(true);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (Selected) Select(false);
        }
    }
}
