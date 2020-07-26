using Engine.Bridges;
using UnityEngine;

namespace Unity.Centers
{
    public class UICenter : MonoBehaviour
    {
        
        protected IBridge _bridge;
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        
        /// <summary>
        /// Public setter for the active bridge.
        /// </summary>
        /// <param name="bridge">The bridge instance to use.</param>
        public void SetBridge(IBridge bridge)
        {
            _bridge = bridge;
        }
    }
}
