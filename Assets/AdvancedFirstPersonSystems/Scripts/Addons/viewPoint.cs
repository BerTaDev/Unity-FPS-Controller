using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BerTaDEV
{
    public class viewPoint : MonoBehaviour
    {
        public bool drawGizmos;
        [Header("Viewpoint")]
        public string PointText = "Press E";
        public Sprite PointIcon;
        public Image ImagePrefab;
        [Range(0.1f, 20)] public float MaxViewRange = 8;
        [Range(0.1f, 20)] public float MaxTextViewRange = 3;

        float Distance;
        TMP_Text ImageText;
        Image ImageUI;
        Camera cam;
        GameObject PlayerController;

        public void SetText(string Text)
        {
            PointText = Text;
            ImageText.text = PointText;
        }
        public void SetIcon(Sprite icon)
        {
            ImageUI.sprite = icon;
        }
        void Start()
        {
            cam = Camera.main;
            PlayerController = GameObject.FindWithTag("Player");
            ImageUI = Instantiate(ImagePrefab, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
            ImageText = ImageUI.GetComponentInChildren<TMP_Text>();
            ImageText.text = PointText;
            ImageUI.sprite = PointIcon;
        }
        private void OnDestroy()
        {
            if (ImageUI)
                Destroy(ImageUI.gameObject);
        }
        private void OnDisable()
        {
            if (ImageUI)
                ImageUI.gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            if (ImageUI)
                ImageUI.gameObject.SetActive(true);
        }
        private void Update()
        {
            ImageUI.transform.position = cam.WorldToScreenPoint(calculateWorldPosition(transform.position, cam));
            Distance = Vector3.Distance(PlayerController.transform.position, transform.position);

            Color TextOpacityColor = ImageText.color;
            TextOpacityColor.a = Mathf.Lerp(TextOpacityColor.a, Distance < MaxTextViewRange ? 1 : 0, 10 * Time.deltaTime);
            ImageText.color = TextOpacityColor;

            Color UIOpacityColor = ImageUI.color;
            UIOpacityColor.a = Mathf.Lerp(UIOpacityColor.a, Distance < MaxViewRange ? 1 : 0, 10 * Time.deltaTime);
            ImageUI.color = UIOpacityColor;

        }
        private Vector3 calculateWorldPosition(Vector3 position, Camera camera)
        {
            Vector3 camNormal = camera.transform.forward;
            Vector3 vectorFromCam = position - camera.transform.position;
            float camNormDot = Vector3.Dot(camNormal, vectorFromCam.normalized);
            if (camNormDot <= 0f)
            {
                float camDot = Vector3.Dot(camNormal, vectorFromCam);
                Vector3 proj = (camNormal * camDot * 1.01f);
                position = camera.transform.position + (vectorFromCam - proj);
            }
            return position;
        }
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (!drawGizmos) return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, MaxTextViewRange);
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, MaxViewRange);

            Gizmos.color = Color.green;
            Vector3 position = transform.position;
            Gizmos.DrawSphere(position, 0.2f);
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.black;
            style.fontSize = 15;
            style.fontStyle = FontStyle.Bold;
            // Scene görünümüne metin ekleme
            UnityEditor.Handles.Label(position + Vector3.up * 0.5f, PointText, style);
        }
#endif
    }
}
