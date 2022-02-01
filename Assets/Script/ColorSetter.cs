/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

public class ColorSetter : MonoBehaviour
{
#region Fields (Inspector Interface)
    [ TitleGroup( "Setup" ), SerializeField ] private Color color;
#endregion

#region Fields (Private)
    private static int SHADER_ID_COLOR = Shader.PropertyToID( "_Color" );

    private Renderer renderer_;
    private MaterialPropertyBlock propertyBlock;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
		renderer_ = GetComponent< Renderer >();
        
		propertyBlock = new MaterialPropertyBlock();
	}
    
    private void OnValidate()
    {
		renderer_ = GetComponent< Renderer >();

		propertyBlock = new MaterialPropertyBlock();

		SetColor();
	}
#endregion

#region API
    // TODO: (OFG) Use the overload you want and delete the other. 

    public void SetColor( Color color )
    {
		renderer_.GetPropertyBlock( propertyBlock );
		propertyBlock.SetColor( SHADER_ID_COLOR, color );
		renderer_.SetPropertyBlock( propertyBlock );
	}

	public void SetColor() // Info: This may be more "Unity-Event-friendly".
	{
        renderer_.GetPropertyBlock( propertyBlock );
		propertyBlock.SetColor( SHADER_ID_COLOR, color );
		renderer_.SetPropertyBlock( propertyBlock );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
