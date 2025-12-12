using UnityEngine;

public abstract class BehaviorTree : MonoBehaviour
{
    protected Node root;
    public Node activeNode;

    private bool treeFinished = false;

    protected abstract void InitializeTree();

    public Animator Animator { get; private set; }

    void Start()
    {
        InitializeTree();
        EvaluateTree();
    }

    private void Awake()
    {
        OnEnable();
    }

    private void OnEnable()
    {
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (activeNode != null)
        {
            activeNode.Tick(Time.deltaTime);
        }
        else if (treeFinished)
        {

            treeFinished = false;
            EvaluateTree();
        }
    }

    public void EvaluateTree()
    {

        activeNode = null;

        if (root != null)
            root.ExecuteAction();
    }

    public void OnTreeFinished()
    {
        treeFinished = true;
        activeNode = null;
    }

    public void Interupt()
    {
        if (activeNode != null)
            activeNode.Interrupt();

        OnTreeFinished();
    }
}