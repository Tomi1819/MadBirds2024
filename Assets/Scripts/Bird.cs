using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    [SerializeField] float maxDragDistance = 4;
    [SerializeField] float launchPower = 150;
    Vector3 startingPosition;
    LineRenderer linerenderer;

    void Start()
    {
        linerenderer = GetComponent<LineRenderer>();
        linerenderer.SetPosition(0, transform.position);
        linerenderer.enabled = false;
        startingPosition = transform.position;
    }

    void OnMouseUp()
    {
        Vector3 directionAndMagnitude = startingPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionAndMagnitude * launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        linerenderer.enabled = false;
    }

    void OnMouseDrag()
    {
        Vector3 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        destination.z = 0;
        if (Vector2.Distance(destination, startingPosition) > maxDragDistance)
        {
            destination = Vector3.MoveTowards(startingPosition, destination, maxDragDistance);
        }
        transform.position = destination;
        linerenderer.SetPosition(1, transform.position);
        linerenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }

        if (FindAnyObjectByType<Enemy>(FindObjectsInactive.Exclude) == null)
        {
            Debug.Log("Game over");
            int levelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(levelToLoad);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(LoadMainMenuWithDelay(5));
    }

    private IEnumerator LoadMainMenuWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync(0);
    }
}
