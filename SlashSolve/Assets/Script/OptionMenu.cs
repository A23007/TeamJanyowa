//�I�v�V�����E�~�b�V�����ȂǕ\�� ���c

using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    [Header("ESC�L�[�ŊJ���I�v�V�������j���[")]
    public GameObject optionCanvas;

    [Header("Space�L�[�ŊJ���X�y�V�������j���[�i���Ԓ�~�Ȃ��j")]
    public GameObject spaceCanvas;

    private bool isOptionOpen = false;
    private bool isSpaceOpen = false;

    void Start()
    {
        if (optionCanvas != null)
            optionCanvas.SetActive(false);

        if (spaceCanvas != null)
            spaceCanvas.SetActive(false);

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptionMenu();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleSpaceMenu();
        }
    }

    private void ToggleOptionMenu()
    {
        if (optionCanvas == null) return;

        isOptionOpen = !isOptionOpen;
        optionCanvas.SetActive(isOptionOpen);

        // Option���j���[���J���Ă���Ƃ��̂ݎ��Ԓ�~
        Time.timeScale = isOptionOpen ? 0f : 1f;
    }

    private void ToggleSpaceMenu()
    {
        if (spaceCanvas == null) return;

        isSpaceOpen = !isSpaceOpen;
        spaceCanvas.SetActive(isSpaceOpen);

        // Space���j���[�͎��Ԃɉe����^���Ȃ��i�������Ȃ��j
    }
}
