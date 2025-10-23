using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 hotspot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;
    public ParticleSystem clickParticle;
    private AudioSource clickSound;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
        clickSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = Camera.main.nearClipPlane + 0.1f;
            clickSound.Play();

            ParticleSystem particle = Instantiate(clickParticle, mousePos, Quaternion.identity);
            particle.Play();
            Destroy(particle.gameObject, particle.main.duration);
        }
    }
}
