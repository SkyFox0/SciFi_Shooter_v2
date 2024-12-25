using UnityEngine;

namespace StarterAssets
{
    public class Ui_Control : MonoBehaviour
    {
        public Canvas X_Canvas;
        public Canvas Scope_Canvas;

        public FirstPersonController FirstPersonController;

        // Start is called once before the first execution of Update after the MonoBehaviour is created

        void Start()
        {
            X_Canvas.enabled = true;
            Scope_Canvas.enabled = false;
        }
        public void Scope_On()
        {
            if (FirstPersonController.Grounded && !FirstPersonController.isReloading)
            {
                X_Canvas.enabled = false;
                Scope_Canvas.enabled = true;
            }            
        }


        public void Scope_Off()
        {
            X_Canvas.enabled = true;
            Scope_Canvas.enabled = false;
        }
    }
}
