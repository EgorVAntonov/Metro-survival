using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeDetector : MonoBehaviour
{
    [SerializeField] private CircleCollider2D area;
    private List<Tree> trees;
    private Tree cashedTree; 

    private void Start()
    {
        trees = new List<Tree>();
        if (area == null)
        {
            GetComponent<CircleCollider2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cashedTree = collision.GetComponent<Tree>();
        if (cashedTree != null)
        {
            trees.Add(cashedTree);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cashedTree = collision.GetComponent<Tree>();
        if (cashedTree != null)
        {
            trees.Remove(cashedTree);
        }
    }

    public Tree GetClosestTree()
    {
        if (trees.Count == 0) return null;

        float minDistance = Mathf.Infinity;
        foreach (var tree in trees)
        {
            if (Vector2.Distance(transform.position, tree.transform.position) < minDistance)
            {
                cashedTree = tree;
            }
        }

        return cashedTree;
    }

    public void ExpandSearchArea()
    {
        area.radius += 0.5f;
    }
}
