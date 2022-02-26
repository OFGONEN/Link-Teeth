/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

public class FillSetter : MonoBehaviour
{
#region Fields (Inspector Interface)
    private float minimumFillValue;
    private float maximumFillValue;
#endregion

#region Fields (Private)
    private static int SHADER_ID_FILLAMOUNT = Shader.PropertyToID( "_FillAmount" );
	private static int SHADER_ID_TOP_COLOR  = Shader.PropertyToID( "_TopColor" );

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
#endregion

#region API
	[ Button() ]
	public void SetupFillRange( float min, float max, Color color )
	{
		minimumFillValue = min;
		maximumFillValue = max;

		renderer_.GetPropertyBlock( propertyBlock );
		propertyBlock.SetColor( SHADER_ID_TOP_COLOR, color );
		renderer_.SetPropertyBlock( propertyBlock );
	}

    public void SetFillRate( float fillRate_Normalized )
    {		
		renderer_.GetPropertyBlock( propertyBlock );
		propertyBlock.SetFloat( SHADER_ID_FILLAMOUNT, ActualFillRate( fillRate_Normalized ) );
		renderer_.SetPropertyBlock( propertyBlock );
	}
#endregion

#region Implementation
	private float ActualFillRate( float fillRate_Normalized )
	{
		return Mathf.Lerp( minimumFillValue, maximumFillValue, fillRate_Normalized );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
	[ ShowInInspector, Range( 0, 1 ) ] private float fillRate_test;

	private void OnValidate()
	{
		renderer_ = GetComponent< Renderer >();

		propertyBlock = new MaterialPropertyBlock();

		SetFillRate( fillRate_test );
	}
#endif
#endregion
}