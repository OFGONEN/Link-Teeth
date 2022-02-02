/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

public class FillSetter : MonoBehaviour
{
#region Fields (Inspector Interface)
    [ TitleGroup( "Setup" ), SerializeField ] private float minimumFillValue;
    [ TitleGroup( "Setup" ), SerializeField ] private float maximumFillValue;
    [ TitleGroup( "Setup" ), SerializeField, Range( 0, 1 ) ] private float fillRate;
#endregion

#region Fields (Private)
    private static int SHADER_ID_FILLAMOUNT = Shader.PropertyToID( "_FillAmount" );

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

		SetFillRate();
	}
#endregion

#region API
    // TODO: (OFG) Use the overload you want and delete the other. 

    public void SetFillRate( float fillRate_Normalized )
    {		
		renderer_.GetPropertyBlock( propertyBlock );
		propertyBlock.SetFloat( SHADER_ID_FILLAMOUNT, ActualFillRate( fillRate_Normalized ) );
		renderer_.SetPropertyBlock( propertyBlock );
	}

	public void SetFillRate() // Info: This may be more "Unity-Event-friendly".
	{
        renderer_.GetPropertyBlock( propertyBlock );
		propertyBlock.SetFloat( SHADER_ID_FILLAMOUNT, ActualFillRate( fillRate ) );
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
#endif
#endregion
}
