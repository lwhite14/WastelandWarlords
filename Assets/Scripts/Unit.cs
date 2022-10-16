using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexPathfinding;

public class Unit : MonoBehaviour
{
    public float health = 10f;
    public float damage = 2f;
    public int fullMovementPoints = 3;
    public float moveSpeed = 4.0f;
    public float sightRange = 5.0f;
    int movementPoints;
    public Animator anim;
    public Animator uiAnim;
    bool isMoving = false;

    GameObject GFX;
    AudioSource audioSource;

    public HexCell cellOn { get; private set; }
    public string unitName { get; set; }

    private void Awake()
    {
        GFX = transform.GetChild(0).gameObject;
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        movementPoints = fullMovementPoints;
        CalcUIState();
        FogOfWar.instance.CalculateVertexAlphas(transform.position, new Vector3(transform.position.x, transform.position.y + 200.0f, transform.position.z), sightRange);
    }

    public void SetCell(HexCell newCell)
    {
        if (cellOn != null)
        {
            cellOn.unit = null;
        }
        cellOn = newCell;
        cellOn.unit = this;
        transform.SetParent(newCell.topTarget);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    }

    public void Move(HexCell newCell)
    {
        PlaySound(1);

        cellOn.unit = null;
        movementPoints = newCell.topTarget.GetComponentInChildren<HexMarker>().movementNode.movementPointsLeft;
        CalcUIState();

        MovementNode toNode = newCell.topTarget.GetComponentInChildren<HexMarker>().movementNode;
        StartCoroutine(LerpUnit(toNode, newCell));
    }

    public void Attack(HexCell attackCell)
    {
        PlaySound(1);

        MovementNode prevNode = attackCell.topTarget.GetComponentInChildren<HexMarker>().movementNode.prevNode;
        HexCell cellToMoveTo = HexGrid.instance.hexCells[prevNode.coordinates.X, prevNode.coordinates.Z];

        cellOn.unit = null;
        cellOn = cellToMoveTo;
        cellOn.unit = this;
        movementPoints = attackCell.topTarget.GetComponentInChildren<HexMarker>().movementNode.movementPointsLeft;
        CalcUIState();

        StartCoroutine(AttackEnemy(prevNode, cellToMoveTo, attackCell.enemy));

    }

    IEnumerator LerpUnit(MovementNode toNode, HexCell newCell)
    {
        isMoving = true;
        transform.SetParent(null);

        List<MovementNode> nodes = new List<MovementNode>();
        nodes.Add(toNode);

        bool cont = true;
        int currentNode = 0;
        while (cont)
        {
            if (nodes[currentNode].prevNode != null)
            {
                nodes.Add(nodes[currentNode].prevNode);
                currentNode++;
            }
            else
            {
                cont = false;
            }
            yield return null;
        }
        nodes.RemoveAt(nodes.Count - 1);
        for (int i = nodes.Count - 1; i >= 0; i--)
        {
            GFX.SetActive(true);
            if (HexGrid.instance.hexCells[nodes[i].coordinates.X, nodes[i].coordinates.Z].topTarget.GetComponentInChildren<Settlement>() != null) { GFX.SetActive(false); }
            if (GFX.activeInHierarchy) { anim.SetBool("isRunning", true); }

            Vector3 startingPosition = transform.position;
            Vector3 endPosition = HexGrid.instance.hexCells[nodes[i].coordinates.X, nodes[i].coordinates.Z].topTarget.position;
            Vector3 newPosition;


            transform.LookAt(HexGrid.instance.hexCells[nodes[i].coordinates.X, nodes[i].coordinates.Z].topTarget, Vector3.up);
            float progress = 0.0f;
            while (progress < 1.0f)
            {
                newPosition = Vector3.Lerp(startingPosition, endPosition, progress);
                transform.position = newPosition;
                progress += Time.deltaTime * moveSpeed;
                yield return null;
            }
            newPosition = Vector3.Lerp(startingPosition, endPosition, 1.0f);
            transform.position = newPosition;
            FogOfWar.instance.CalculateVertexAlphas(transform.position, new Vector3(transform.position.x, transform.position.y + 200.0f, transform.position.z), sightRange);
            if (HexGrid.instance.hexCells[nodes[i].coordinates.X, nodes[i].coordinates.Z].collectable != null)
            {
                HexGrid.instance.hexCells[nodes[i].coordinates.X, nodes[i].coordinates.Z].collectable.PickUp();
            }
            yield return null;
        }

        cellOn = newCell;
        cellOn.unit = this;
        transform.SetParent(newCell.topTarget);
        anim.SetBool("isRunning", false);
        transform.localPosition = new Vector3(0, 0, 0);
        isMoving = false;
        if (GameState.CellSelected == newCell)
        {
            Select();
        }
        yield return null;
    }

    IEnumerator AttackEnemy(MovementNode toNode, HexCell newCell, Enemy enemy)
    {
        yield return LerpUnit(toNode, newCell);
        transform.LookAt(HexGrid.instance.hexCells[enemy.cellOn.coordinates.X, enemy.cellOn.coordinates.Z].topTarget, Vector3.up);
        anim.Play("Attack");
        enemy.GiveDamage(damage);
        MasterUI.instance.UpdateAllUI();
        yield return null;
    }

    public void Select()
    {
        MasterUI.instance.UpdateUnitPanel(this, null);
        PlaySound(0);
        if (!isMoving)
        {
            GameState.UnitSelected = this;
            MovementNode[] movementNodes = MovementFinder.DisplayMovement(movementPoints, cellOn);
            foreach (MovementNode node in movementNodes)
            {
                if (node.coordinates != cellOn.coordinates)
                {
                    if (HexGrid.instance.hexCells[node.coordinates.X, node.coordinates.Z].enemy == null)
                    {
                        GameObject tempMovementMarker = Instantiate<GameObject>(ResourceFactory.MovementMarker);
                        tempMovementMarker.transform.SetParent(HexGrid.instance.hexCells[node.coordinates.X, node.coordinates.Z].topTarget);
                        GameState.CellsMovement.Add(HexGrid.instance.hexCells[node.coordinates.X, node.coordinates.Z]);
                        tempMovementMarker.transform.localPosition = new Vector3(0, 0.0f, 0);
                        tempMovementMarker.GetComponent<HexMarker>().movementNode = node;
                    }
                    else
                    {
                        GameObject tempAttackMarker = Instantiate<GameObject>(ResourceFactory.AttackMarker);
                        tempAttackMarker.transform.SetParent(HexGrid.instance.hexCells[node.coordinates.X, node.coordinates.Z].topTarget);
                        GameState.CellsAttack.Add(HexGrid.instance.hexCells[node.coordinates.X, node.coordinates.Z]);
                        tempAttackMarker.transform.localPosition = new Vector3(0, 0.0f, 0);
                        tempAttackMarker.GetComponent<HexMarker>().movementNode = node;
                    }
                }
            }
        }
    }

    public void ResetMovementPoints()
    {
        movementPoints = fullMovementPoints;
        CalcUIState();
        if (GameState.UnitSelected == this)
        {
            foreach (GameObject marker in GameObject.FindGameObjectsWithTag("MovementMarker"))
            {
                Destroy(marker);
            }
            Select();
        }
    }

    public void GiveDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameState.Units.Remove(this);
        this.cellOn.unit = null;
        if (TutorialManager.instance != null) { TutorialManager.instance.UnitDied(); }
        Destroy(gameObject);
    }

    void CalcUIState()
    {
        if (movementPoints > 0) { uiAnim.SetBool("hasMovesLeft", true); }
        else { uiAnim.SetBool("hasMovesLeft", false); }
    }

    int RandomSound()
    {
        System.Random random = new System.Random();
        return random.Next(0, 3);
    }

    void PlaySound(int type = 0)
    {
        audioSource.Stop();
        if (type == 0) { audioSource.clip = ResourceFactory.Select[RandomSound()]; }
        if (type == 1) { audioSource.clip = ResourceFactory.Command[RandomSound()]; }
        audioSource.Play();
    }
}
