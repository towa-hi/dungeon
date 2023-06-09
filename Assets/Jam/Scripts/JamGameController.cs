using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamGameController : MonoBehaviour
{
    private static JamGameController ins;
    public static JamGameController instance {get{return ins;}}
    
    public int currentStars;
    public int currentMana;
    // list of all jampawns
    public int roomNumber;
    public FormDef currentForm;

    public JamPlayerController playerController;
    public JamMapController mapController;
    public Dictionary<Vector2Int, FormDef> formData;

    public InfoPanel infoPanel;
    public PlayerSelectPanel selectPanel;
    
    public Sprite liaSprite;
    public Sprite nasaSprite;
    public Sprite pippaSprite;
    public Sprite tenmaSprite;
    public Sprite ioriSprite;
    public Sprite urukaSprite;
    public Sprite michiruSprite;
    public Sprite lumiSprite;
    public Sprite muumiSprite;

    public WinScreen winScreen;
    public WinScreen loseScreen;
    public GameObject readMe;

    public void OpenReadme()
    {
        readMe.SetActive(true);
    }

    public void CloseReadme()
    {
        readMe.SetActive(false);
    }
    private void Awake()
    {
        // boilerplate singleton code
        
        // if exists already
        if (ins != null && ins != this)
        {
            // kill self
            Destroy(this.gameObject);
            Debug.LogError("MainMenuController encountered duplicate singleton, deleted self");
        }
        ins = this;
    }
    
    void Start()
    {
        formData = new Dictionary<Vector2Int, FormDef>();
        FormLia lia = new FormLia();
        formData.Add(lia.formId, lia);
        FormNasa nasa = new FormNasa();
        formData.Add(nasa.formId, nasa);
        FormPippa pippa = new FormPippa();
        formData.Add(pippa.formId,pippa);
        FormTenma tenma = new FormTenma();
        formData.Add(tenma.formId,tenma);
        FormIori iori = new FormIori();
        formData.Add(iori.formId,iori);
        FormUruka uruka = new FormUruka();
        formData.Add(uruka.formId,uruka);
        FormMichiru michiru = new FormMichiru();
        formData.Add(michiru.formId,michiru);
        FormLumi lumi = new FormLumi();
        formData.Add(lumi.formId,lumi);
        FormMuumi muumi = new FormMuumi();
        formData.Add(muumi.formId,muumi);

        //always start as lia
        currentForm = lia;
        
        playerController.Initialize();
        mapController.Initialize(0);
        infoPanel.Initialize();
        selectPanel.Initialize();
    }


    public void NextLevel()
    {
        if (mapController.currentLevel.levelNumber + 1 == mapController.levelList.Count)
        {
            // YOU WIN
            winScreen.Initialize();
            return;
        }
        mapController.Initialize(mapController.currentLevel.levelNumber + 1);
        
    }

    public void LoseGame()
    {

        loseScreen.Initialize();
        return;
        
    }

    public void ChangeForm(Vector2Int dir)
    {
        Vector2Int formPos = currentForm.formId;
        if (formData.ContainsKey(formPos + dir))
        {
            currentForm = formData[formPos + dir];
        }
        else
        {
            Vector2Int invalidFormPos = formPos + dir;
            if (invalidFormPos.x < 0)
            {
                invalidFormPos.x = 2;
            }

            if (invalidFormPos.x > 2)
            {
                invalidFormPos.x = 0;
            }

            if (invalidFormPos.y < 0)
            {
                invalidFormPos.y = 2;
            }

            if (invalidFormPos.y > 2)
            {
                invalidFormPos.y = 0;
            }
            currentForm = formData[invalidFormPos];
        }
    }
    
}

public abstract class FormDef
{
    public Vector2Int formId;
    public Sprite formSprite;
    public string formName;
    public int manaCost;
    public int maxHp;
    public int remainingHp;

    protected virtual void Init()
    {
        
    }

    public virtual void Target()
    {
        
    }

    public virtual void Attack(Vector2Int dir)
    {
        
    }
}

public class FormLia : FormDef
{
    public FormLia()
    {
        // fast
        formId =  new Vector2Int(0, 0);
        formSprite = JamGameController.instance.liaSprite;
        formName = "Lia";
        manaCost = 2;
        maxHp = 2;
        remainingHp = maxHp;
    }

    public override void Target()
    {
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.up))
        {
            cell.SetSignDirection(Vector2Int.up);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.down))
        {
            cell.SetSignDirection(Vector2Int.down);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.right))
        {
            cell.SetSignDirection(Vector2Int.right);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.left))
        {
            cell.SetSignDirection(Vector2Int.left);
            cell.SetCircle(true);
        }
    }

    public List<JamCell> ListOfAttackedCells(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int currentPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[currentPos];
        List<JamCell> attackedTiles = new List<JamCell>();
        if (dir == Vector2Int.up)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.up);
            if (attackedCell)
            {
                if (!currentCell.hasUpWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.down)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.down);
            if (attackedCell)
            {
                if (!attackedCell.hasUpWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.right)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.right);
            if (attackedCell)
            {
                if (!currentCell.hasRightWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.left)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.left);
            if (attackedCell)
            {
                if (!attackedCell.hasRightWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }

        return attackedTiles;
    }
    public override void Attack(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int playerPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[playerPos];
        List<JamCell> attackedCells = ListOfAttackedCells(dir);
        bool enemyFound = false;
        foreach (JamCell cell in attackedCells)
        {
            JamEntity enemy = JamGameController.instance.mapController.GetEnemyAtLocation(cell.pos);
            if (enemy != null)
            {
                enemyFound = true;
                mapController.KillEnemy(enemy);
            }
        }

        if (enemyFound)
        {
            mapController.currentLevel.MoveEntity(mapController.playerEntity, playerPos + dir);
            JamGameController.instance.currentMana -= manaCost;
            mapController.shouldChangeFormAtEndOfTurn = true;
            mapController.changeFormDir = dir;
            mapController.NextTurn();
        }
    }
}

public class FormNasa : FormDef
{
    public FormNasa()
    {
        formId =  new Vector2Int(1, 0);
        formSprite = JamGameController.instance.nasaSprite;
        formName = "Nasa";
        manaCost = 2;
        maxHp = 2;
        remainingHp = maxHp;
    }

    public override void Target()
    {
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.up))
        {
            cell.SetSignDirection(Vector2Int.up);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.down))
        {
            cell.SetSignDirection(Vector2Int.down);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.right))
        {
            cell.SetSignDirection(Vector2Int.right);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.left))
        {
            cell.SetSignDirection(Vector2Int.left);
            cell.SetCircle(true);
        }
    }

    public List<JamCell> ListOfAttackedCells(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int currentPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[currentPos];
        List<JamCell> attackedTiles = new List<JamCell>();
        if (dir == Vector2Int.up)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.up);
            if (attackedCell)
            {
                if (!currentCell.hasUpWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.down)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.down);
            if (attackedCell)
            {
                if (!attackedCell.hasUpWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.right)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.right);
            if (attackedCell)
            {
                if (!currentCell.hasRightWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.left)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.left);
            if (attackedCell)
            {
                if (!attackedCell.hasRightWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }

        return attackedTiles;
    }
    public override void Attack(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int playerPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[playerPos];
        List<JamCell> attackedCells = ListOfAttackedCells(dir);
        bool enemyFound = false;
        foreach (JamCell cell in attackedCells)
        {
            JamEntity enemy = JamGameController.instance.mapController.GetEnemyAtLocation(cell.pos);
            if (enemy != null)
            {
                enemyFound = true;
                mapController.KillEnemy(enemy);
            }
        }

        if (enemyFound)
        {
            mapController.currentLevel.MoveEntity(mapController.playerEntity, playerPos + dir);
            JamGameController.instance.currentMana -= manaCost;
            mapController.shouldChangeFormAtEndOfTurn = true;
            mapController.changeFormDir = dir;
            mapController.NextTurn();
        }
    }
}

public class FormPippa : FormDef
{
    public FormPippa()
    {
        formId =  new Vector2Int(2, 0);
        formSprite = JamGameController.instance.pippaSprite;
        formName = "Pippa";
        manaCost = 2;
        maxHp = 2;
        remainingHp = maxHp;
    }

    public override void Target()
    {
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.up))
        {
            cell.SetSignDirection(Vector2Int.up);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.down))
        {
            cell.SetSignDirection(Vector2Int.down);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.right))
        {
            cell.SetSignDirection(Vector2Int.right);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.left))
        {
            cell.SetSignDirection(Vector2Int.left);
            cell.SetCircle(true);
        }
    }

    public List<JamCell> ListOfAttackedCells(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int currentPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[currentPos];
        List<JamCell> attackedTiles = new List<JamCell>();
        if (dir == Vector2Int.up)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.up);
            if (attackedCell)
            {
                attackedTiles.Add(attackedCell);
            }
        }
        if (dir == Vector2Int.down)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.down);
            if (attackedCell)
            {
                attackedTiles.Add(attackedCell);
            }
        }
        if (dir == Vector2Int.right)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.right);
            if (attackedCell)
            {
                attackedTiles.Add(attackedCell);
            }
        }
        if (dir == Vector2Int.left)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.left);
            if (attackedCell)
            {
                attackedTiles.Add(attackedCell);
            }
        }

        return attackedTiles;
    }
    public override void Attack(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int playerPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[playerPos];
        List<JamCell> attackedCells = ListOfAttackedCells(dir);
        foreach (JamCell cell in attackedCells)
        {
            JamEntity enemy = JamGameController.instance.mapController.GetEnemyAtLocation(cell.pos);
            if (enemy != null)
            {
                mapController.KillEnemy(enemy);
            }
        }
        mapController.currentLevel.MoveEntity(mapController.playerEntity, playerPos + dir);
        JamGameController.instance.currentMana -= manaCost;
        mapController.shouldChangeFormAtEndOfTurn = true;
        mapController.changeFormDir = dir;
        mapController.NextTurn();
    }
}

public class FormTenma : FormDef
{
    public FormTenma()
    {
        formId =  new Vector2Int(0, 1);
        formSprite = JamGameController.instance.tenmaSprite;
        formName = "Tenma";
        manaCost = 2;
        maxHp = 2;
        remainingHp = maxHp;
    }

    public override void Target()
    {
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.up))
        {
            cell.SetSignDirection(Vector2Int.up);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.down))
        {
            cell.SetSignDirection(Vector2Int.down);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.right))
        {
            cell.SetSignDirection(Vector2Int.right);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.left))
        {
            cell.SetSignDirection(Vector2Int.left);
        }
    }

    public List<JamCell> ListOfAttackedCells(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int currentPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[currentPos];
        List<JamCell> attackedTiles = new List<JamCell>();
        if (dir == Vector2Int.up)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.up);
            if (attackedCell)
            {
                if (!currentCell.hasUpWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.down)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.down);
            if (attackedCell)
            {
                if (!attackedCell.hasUpWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.right)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.right);
            if (attackedCell)
            {
                if (!currentCell.hasRightWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.left)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.left);
            if (attackedCell)
            {
                if (!attackedCell.hasRightWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }

        return attackedTiles;
    }
    public override void Attack(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int playerPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[playerPos];
        List<JamCell> attackedCells = ListOfAttackedCells(dir);
        bool enemyFound = false;
        foreach (JamCell cell in attackedCells)
        {
            JamEntity enemy = JamGameController.instance.mapController.GetEnemyAtLocation(cell.pos);
            if (enemy != null)
            {
                enemyFound = true;
                mapController.KillEnemy(enemy);
            }
        }

        if (enemyFound)
        {
            JamGameController.instance.currentMana -= manaCost;
            mapController.shouldChangeFormAtEndOfTurn = true;
            mapController.changeFormDir = dir;
            mapController.NextTurn();
        }
    }
}

public class FormIori : FormDef
{
    public FormIori()
    {
        formId =  new Vector2Int(1, 1);
        formSprite = JamGameController.instance.ioriSprite;
        formName = "Iori";
        manaCost = 2;
        maxHp = 2;
        remainingHp = maxHp;
    }

    public override void Target()
    {
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.up))
        {
            cell.SetSignDirection(Vector2Int.up);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.down))
        {
            cell.SetSignDirection(Vector2Int.down);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.right))
        {
            cell.SetSignDirection(Vector2Int.right);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.left))
        {
            cell.SetSignDirection(Vector2Int.left);
            cell.SetCircle(true);
        }
    }

    public List<JamCell> ListOfAttackedCells(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int currentPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[currentPos];
        List<JamCell> attackedTiles = new List<JamCell>();
        if (dir == Vector2Int.up)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.up);
            if (attackedCell)
            {
                if (!currentCell.hasUpWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.down)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.down);
            if (attackedCell)
            {
                if (!attackedCell.hasUpWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.right)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.right);
            if (attackedCell)
            {
                if (!currentCell.hasRightWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.left)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.left);
            if (attackedCell)
            {
                if (!attackedCell.hasRightWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }

        return attackedTiles;
    }
    public override void Attack(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int playerPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[playerPos];
        List<JamCell> attackedCells = ListOfAttackedCells(dir);
        bool enemyFound = false;
        foreach (JamCell cell in attackedCells)
        {
            JamEntity enemy = JamGameController.instance.mapController.GetEnemyAtLocation(cell.pos);
            if (enemy != null)
            {
                enemyFound = true;
                mapController.KillEnemy(enemy);
            }
        }

        if (enemyFound)
        {
            mapController.currentLevel.MoveEntity(mapController.playerEntity, playerPos + dir);
            JamGameController.instance.currentMana -= manaCost;
            mapController.shouldChangeFormAtEndOfTurn = true;
            mapController.changeFormDir = dir;
            mapController.NextTurn();
        }
    }
}

public class FormUruka : FormDef
{
    public FormUruka()
    {
        formId =  new Vector2Int(2, 1);
        formSprite = JamGameController.instance.urukaSprite;
        formName = "Uruka";
        manaCost = 2;
        maxHp = 2;
        remainingHp = maxHp;
    }

    public override void Target()
    {
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.up))
        {
            cell.SetSignDirection(Vector2Int.up);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.down))
        {
            cell.SetSignDirection(Vector2Int.down);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.right))
        {
            cell.SetSignDirection(Vector2Int.right);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.left))
        {
            cell.SetSignDirection(Vector2Int.left);
            cell.SetCircle(true);
        }
    }

    public List<JamCell> ListOfAttackedCells(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int currentPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[currentPos];
        List<JamCell> attackedTiles = new List<JamCell>();
        if (dir == Vector2Int.up)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.up);
            if (attackedCell)
            {
                if (!currentCell.hasUpWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.down)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.down);
            if (attackedCell)
            {
                if (!attackedCell.hasUpWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.right)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.right);
            if (attackedCell)
            {
                if (!currentCell.hasRightWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.left)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.left);
            if (attackedCell)
            {
                if (!attackedCell.hasRightWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }

        return attackedTiles;
    }
    public override void Attack(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int playerPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[playerPos];
        List<JamCell> attackedCells = ListOfAttackedCells(dir);
        bool enemyFound = false;
        foreach (JamCell cell in attackedCells)
        {
            JamEntity enemy = JamGameController.instance.mapController.GetEnemyAtLocation(cell.pos);
            if (enemy != null)
            {
                enemyFound = true;
                mapController.KillEnemy(enemy);
            }
        }

        if (enemyFound)
        {
            mapController.currentLevel.MoveEntity(mapController.playerEntity, playerPos + dir);
            JamGameController.instance.currentMana -= manaCost;
            mapController.shouldChangeFormAtEndOfTurn = true;
            mapController.changeFormDir = dir;
            mapController.NextTurn();
        }
    }
}

public class FormMichiru : FormDef
{
    // dervish
    public FormMichiru()
    {
        formId =  new Vector2Int(0, 2);
        formSprite = JamGameController.instance.michiruSprite;
        formName = "Michiru";
        manaCost = 2;
        maxHp = 2;
        remainingHp = maxHp;
    }

    public override void Target()
    {
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.up))
        {
            cell.SetSignDirection(Vector2Int.up);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.down))
        {
            cell.SetSignDirection(Vector2Int.down);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.right))
        {
            cell.SetSignDirection(Vector2Int.right);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.left))
        {
            cell.SetSignDirection(Vector2Int.left);
            cell.SetCircle(true);
        }
    }

    public List<JamCell> ListOfAttackedCells(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int currentPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[currentPos];
        List<JamCell> attackedTiles = new List<JamCell>();
        if (dir == Vector2Int.up)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.up);
            if (attackedCell)
            {
                if (!currentCell.hasUpWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.down)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.down);
            if (attackedCell)
            {
                if (!attackedCell.hasUpWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.right)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.right);
            if (attackedCell)
            {
                if (!currentCell.hasRightWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.left)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.left);
            if (attackedCell)
            {
                if (!attackedCell.hasRightWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }

        return attackedTiles;
    }
    public override void Attack(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int playerPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[playerPos];
        List<JamCell> attackedCells = ListOfAttackedCells(dir);
        bool enemyFound = false;
        foreach (JamCell cell in attackedCells)
        {
            JamEntity enemy = JamGameController.instance.mapController.GetEnemyAtLocation(cell.pos);
            if (enemy != null)
            {
                enemyFound = true;
                mapController.KillEnemy(enemy);
            }
        }

        if (enemyFound)
        {
            mapController.currentLevel.MoveEntity(mapController.playerEntity, playerPos + dir);
            JamGameController.instance.currentMana -= manaCost;
            mapController.shouldChangeFormAtEndOfTurn = true;
            mapController.changeFormDir = dir;
            mapController.NextTurn();
        }
    }
}

public class FormLumi : FormDef
{
    public FormLumi()
    {
        formId =  new Vector2Int(1, 2);
        formSprite = JamGameController.instance.lumiSprite;
        formName = "Lumi";
        manaCost = 2;
        maxHp = 2;
        remainingHp = maxHp;
    }

    public override void Target()
    {
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.up))
        {
            cell.SetSignDirection(Vector2Int.up);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.down))
        {
            cell.SetSignDirection(Vector2Int.down);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.right))
        {
            cell.SetSignDirection(Vector2Int.right);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.left))
        {
            cell.SetSignDirection(Vector2Int.left);
            cell.SetCircle(true);
        }
    }

    public List<JamCell> ListOfAttackedCells(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int currentPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[currentPos];
        List<JamCell> attackedTiles = new List<JamCell>();
        if (dir == Vector2Int.up)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.up);
            if (attackedCell)
            {
                if (!currentCell.hasUpWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.down)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.down);
            if (attackedCell)
            {
                if (!attackedCell.hasUpWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.right)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.right);
            if (attackedCell)
            {
                if (!currentCell.hasRightWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.left)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.left);
            if (attackedCell)
            {
                if (!attackedCell.hasRightWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }

        return attackedTiles;
    }
    public override void Attack(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int playerPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[playerPos];
        List<JamCell> attackedCells = ListOfAttackedCells(dir);
        bool enemyFound = false;
        foreach (JamCell cell in attackedCells)
        {
            JamEntity enemy = JamGameController.instance.mapController.GetEnemyAtLocation(cell.pos);
            if (enemy != null)
            {
                enemyFound = true;
                mapController.KillEnemy(enemy);
            }
        }

        if (enemyFound)
        {
            mapController.currentLevel.MoveEntity(mapController.playerEntity, playerPos + dir);
            JamGameController.instance.currentMana -= manaCost;
            JamGameController.instance.currentStars += 1;
            mapController.shouldChangeFormAtEndOfTurn = true;
            mapController.changeFormDir = dir;
            mapController.NextTurn();
        }
    }
}

public class FormMuumi : FormDef
{
    public FormMuumi()
    {
        formId =  new Vector2Int(2, 2);
        formSprite = JamGameController.instance.muumiSprite;
        formName = "Muumi";
        manaCost = 3;
        maxHp = 3;
        remainingHp = maxHp;
    }
 
    public override void Target()
    {
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.up))
        {
            cell.SetSignDirection(Vector2Int.up);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.down))
        {
            cell.SetSignDirection(Vector2Int.down);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.right))
        {
            cell.SetSignDirection(Vector2Int.right);
            cell.SetCircle(true);
        }
        foreach (JamCell cell in ListOfAttackedCells(Vector2Int.left))
        {
            cell.SetSignDirection(Vector2Int.left);
            cell.SetCircle(true);
        }
    }

    public List<JamCell> ListOfAttackedCells(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int currentPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[currentPos];
        List<JamCell> attackedTiles = new List<JamCell>();
        if (dir == Vector2Int.up)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.up);
            if (attackedCell)
            {
                if (!currentCell.hasUpWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.down)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.down);
            if (attackedCell)
            {
                if (!attackedCell.hasUpWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.right)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.right);
            if (attackedCell)
            {
                if (!currentCell.hasRightWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }
        if (dir == Vector2Int.left)
        {
            JamCell attackedCell = mapController.GetCell(currentPos + Vector2Int.left);
            if (attackedCell)
            {
                if (!attackedCell.hasRightWall)
                {
                    attackedTiles.Add(attackedCell);
                }
            }
        }

        return attackedTiles;
    }
    public override void Attack(Vector2Int dir)
    {
        JamMapController mapController = JamGameController.instance.mapController;
        Vector2Int playerPos = mapController.playerEntity.pos;
        JamCell currentCell = mapController.currentLevel.cellDictionary[playerPos];
        List<JamCell> attackedCells = ListOfAttackedCells(dir);
        bool enemyFound = false;
        foreach (JamCell cell in attackedCells)
        {
            JamEntity enemy = JamGameController.instance.mapController.GetEnemyAtLocation(cell.pos);
            if (enemy != null)
            {
                enemyFound = true;
                mapController.KillEnemy(enemy);
            }
        }

        if (enemyFound)
        {
            mapController.currentLevel.MoveEntity(mapController.playerEntity, playerPos + dir);
            JamGameController.instance.currentMana -= manaCost;
            mapController.shouldChangeFormAtEndOfTurn = true;
            mapController.changeFormDir = dir;
            mapController.NextTurn();
        }
    }
}