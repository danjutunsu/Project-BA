using UnityEngine;

public class TerrainShaderController : MonoBehaviour
{
    public Material terrainMaterial; // reference to the terrain material

    private void Update()
    {
        // get the current player position
        Vector3 playerPos = transform.position;

        // set the player position to the material
        terrainMaterial.SetVector("_CharacterPos", playerPos);
    }
}