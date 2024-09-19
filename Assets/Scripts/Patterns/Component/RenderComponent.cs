using Patterns.Component.Interfaces;

namespace Patterns.Component
{
    public class RenderComponent:IRenderComponent
    {
        public void Render(AGObject obj)
        {
            obj.gameObject.transform.position = obj.cam.ScreenToWorldPoint(obj.position);
        }
    }
}