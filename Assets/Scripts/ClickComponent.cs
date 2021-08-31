using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ziggurat.Units
{
    public class ClickComponent : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Material material;
        private MeshRenderer _mesh;
        private Material[] _meshMaterials = new Material[3];

        private bool IsSelected = false;

        public delegate void ClickEventHandler(ClickComponent component);

        public event ClickEventHandler OnClickEventHandler;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsSelected)
            {
                AddAdditionalMaterial(material);
                IsSelected = true;
            }
            else
            {
                RemoveAdditionalMaterial();
                IsSelected = false;
            }
        }

        public void AddAdditionalMaterial(Material material, int index = 1)
        {
            if (index < 1 || index > 2)
            {
                return;
            }
            _meshMaterials[index] = material;
            _mesh.materials = _meshMaterials.Where(t => t != null).ToArray();
        }

        public void RemoveAdditionalMaterial(int index = 1)
        {
            if (index < 1 || index > 2)
            {
                return;
            }
            _meshMaterials[index] = null;
            _mesh.materials = _meshMaterials.Where(t => t != null).ToArray();
        }

        protected virtual void Start()
        {
            _mesh = GetComponent<MeshRenderer>();
            _meshMaterials[0] = _mesh.material;
        }
    }
}
