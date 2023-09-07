using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public FootstepAudio defaultFootstep;

    public FootstepAudio GravelAudio;
    public FootstepAudio GrassAudio;
    public FootstepAudio StoneAudio;
    public FootstepAudio WoodAudio;

    private FootstepAudio currentAudio;

    private LayerMask layerMask = 1 << 0 | 1 << 16 | 1 << 3;
    private CharacterController controller;
    private WeFly.Character_Input input;

    public bool debug = false;

    private int firstSound = 2;

    void Start()
    {
        currentAudio = defaultFootstep;
        controller = GetComponentInParent<CharacterController>();
        input = GetComponentInParent<WeFly.Character_Input>();
        InvokeRepeating("CheckGround",0f,1f);
    }

    public void PlayFootstep()
    {
        if (firstSound != 0)
        {
            firstSound--;
            return;
        }

        if (debug) Debug.Log("Footstep!");
        currentAudio.PlayFootstep();
    }

    void SetTerrainAudio(int index)
    {
        if (debug) Debug.Log("Footstep terrain index is " + index);
        switch(index)
        {
            case 1:
                currentAudio = StoneAudio;
                break;
            case 3:
            case 4:
                currentAudio = GravelAudio;
                break;
            case 5:
            case 6:
            case 7:
            case 2:
                currentAudio = GrassAudio;
                break;
            default:
                currentAudio = defaultFootstep;
                break;
        }
    }

    void SetRendererAudio(string tag)
    {
        if(debug) Debug.Log("Footstep tag is " + tag);
        switch(tag)
        {
            case "StoneTexture":
                currentAudio = StoneAudio;
                break;
            default:
                currentAudio = StoneAudio;
                break;
        }
    }

    void CheckGround()
    {
        if (controller.isGrounded)
        {
            if (Physics.Raycast(transform.position, Vector3.down,out RaycastHit hit, 3f,layerMask))
            {
                if (hit.collider.TryGetComponent<Renderer>(out Renderer renderer))
                {
                    SetRendererAudio(renderer.transform.tag);
                }
                else if (hit.collider.TryGetComponent<Terrain>(out Terrain terrain))
                {
                    Vector3 terrainPosition = hit.point - terrain.transform.position;
                    Vector3 splatMapPosition = new Vector3(terrainPosition.x / terrain.terrainData.size.x,
                    0,
                    terrainPosition.z / terrain.terrainData.size.z
                    );

                    int x = Mathf.FloorToInt(splatMapPosition.x * terrain.terrainData.alphamapWidth);
                    int z = Mathf.FloorToInt(splatMapPosition.z * terrain.terrainData.alphamapHeight);

                    float[,,] alphaMap = terrain.terrainData.GetAlphamaps(x,z,1,1);

                    int primaryIndex = 0;

                    for (int i = 1; i <alphaMap.Length; i++)
                    {
                        if (alphaMap[0,0,i] > alphaMap [0,0,primaryIndex])
                        {
                            primaryIndex = i;
                        }
                    }
                    
                    SetTerrainAudio(primaryIndex);
                }
                
            }
        }
    }
}
