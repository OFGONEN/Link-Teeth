/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class ColorSetter_Update : MonoBehaviour
{
#region Fields (Inspector Interface)
	[TitleGroup( "Setup" ), SerializeField] private Color color;
#endregion

#region Fields (Private)
	private static int SHADER_ID_COLOR = Shader.PropertyToID( "_Color" );

	private Renderer _renderer;
	private MaterialPropertyBlock propertyBlock;
#endregion

#region Properties
#endregion

#region Unity API
	private void Awake()
	{
		_renderer = GetComponent< Renderer >();

		propertyBlock = new MaterialPropertyBlock();
	}

    private void Update()
    {
		SetColor();
	}
#endregion

#region API
	public void SetColor( Color color )
	{
		this.color = color;

		SetColor();
	}

	public void SetColor() // Info: This may be more "Unity-Event-friendly".
	{
		color = color.SetAlpha( _renderer.sharedMaterial.color.a );
		_renderer.GetPropertyBlock( propertyBlock );
		propertyBlock.SetColor( SHADER_ID_COLOR, color );
		_renderer.SetPropertyBlock( propertyBlock );
	}

	public void SetFinalColor()
	{
		color = color.SetAlpha( 1 );
		_renderer.GetPropertyBlock( propertyBlock );
		propertyBlock.SetColor( SHADER_ID_COLOR, color );
		_renderer.SetPropertyBlock( propertyBlock );

		enabled = false;
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}