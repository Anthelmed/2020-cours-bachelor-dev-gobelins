# 2020-cours-bachelor-dev-gobelins

Unity 2018.3.14f1 - Built-in pipeline

### Plugins
- Cinemachine : https://docs.unity3d.com/Packages/com.unity.cinemachine@2.5/manual/index.html
- DoTween : http://dotween.demigiant.com/documentation.php
- Post Processing Stack v2 : https://docs.unity3d.com/Packages/com.unity.postprocessing@2.3/manual/index.html (Pour ceux qui utilisent les pipelines URP ou HDRP le post processing est déjà inclus)

### Components

- Transform (représente la position / rotation / scale de votre gameObject)
- Box, Sphere, Mesh Collider (représente une version simplifier de votre mesh, utile pour la physique)
- Rigidbody (permet à votre gameObject d'être influencer par la physique / gravité)

## Code

### Variables

```C#
  //Visible depuis l'éditeur (🙅‍ pas une best practice vu que la valeur peut être modifiée depuis un autre script)
  public float speed = 1f;
  
  //Visibile depuis l'éditeur (👍 best practice)
  [SerializeField] private float speed = 1f;
  
  //Peut aussi s'écrire m_defaultPosition ou defaultPosition, c'est juste une convention de nommage
  private Vector3 _defaultPosition;
```

### Methodes

#### Unity Events

```C#
  //Appeler tout au début, avant que la scène soit complètement fini de load
  private void Awake() { ... }
  
  //Appeler après que la scène soit fini de load
  private void Start() { ... }
  
  //Appeler chaque frame
  private void Update() { ... }
  
  //Appeler à la fin de chaque frame (bon pour tout ce qui touche à la caméra)
  private void LateUpdate() { ... }
  
  //Appeler à interval régulier (bon pour la physique)
  private void FixedUpdate() { ... }
  
  //Appeler quand un collider avec un rigidbody entre en contact avec un second collider (le second collider doit avoir la case is Trigger de cocher)
  private void OnTriggerEnter(Collider other) { ... }
  private void OnTriggerExit(Collider other) { ... }
```


### Input

```C#
  //Permet de recevoir une valeur en -1 et 1 en fonction de l'input (WASQ, Arrow, controller stick)
  //🛑 Avec le nouveau système d'input qui en est preview cette méthode risque de disparaitre soon or later
  Input.GetAxis("Horizontal");

  //Retourne vrai tant que la touche espace est appuyer
  Input.GetKey(KeyCode.Space)
```

### Vectors

```C#
  Vector3.up == new Vector3(0f, 1f, 0f);
  Vector3.down == new Vector3(0f, -1f, 0f);
```

### Physics

```C#
  Rigidbody rigidbody;

  //Ajoute une force à votre rigidbody (oui c'est dans le nom)
  rigidbody.AddForce(Vector3.up * 10f);
  
  Vector3 offset;
  
  //Déplace votre rigidbody à une position 
  //🛑 la position est donnée en world space, c'est pour cela que vous devez ajouter la position de votre transform avec l'offset d'où vous voulez aller
  rigidbody.MovePosition(transform.position + offset);
  
  //Envoi un rayon dans la direction que vous précisez, si le rayon touche une surface vous pouvez checker à quel distance à eu lieu cet impact
  Physics.Raycast(transform.position, Vector3.down, out var hit, Mathf.Infinity);
```

### Time
```C#
  //Donne l'interval entre cette frame et la précédente
  Time.deltaTime
  
  //Donne l'interval entre deux call d'update de la physique (Il peut être modifié dans la paramètre de Unity)
  Time.fixedDeltaTime
```

## Pour aller plus loins

### MonoBehaviour
```C#
  //Classe par défault dans Unity, elle doit être attaché à un gameObject de la scène
  public class PressurePlate : MonoBehaviour
  {
      private void Start()
      {

      }
  }

```

### Classe static
```C#
  //Une classe static permet d'être appeler depuis n'importe quel script et n'a pas besoin d'exister dans une scène
  public static class GameManager
  {
      public static int startPlayerHealth = 100;
      
      private static int _currentPlayerHealth;

      public delegate void PlayerHealthEventHandler();
      
      public static event PlayerHealthEventHandler PlayerHealthChangeEvent;
      public static event PlayerHealthEventHandler PlayerDieEvent;

      public static void DecreasePlayerHealth(int value)
      {
          _currentPlayerHealth = Mathf.Max(_currentPlayerHealth - value, 0);
          
          if (_currentPlayerHealth == 0)
          {
            //En une seule ligne PlayerHealthChangeEvent?.Invoke();
          if (PlayerDieEvent != null)
          {
              PlayerDieEvent();
          }
      
          if (PlayerHealthChangeEvent != null)
          {
              PlayerHealthChangeEvent();
          }
      } 
  }
```

### Class singleton
```C#
  //Un singleton permet d'être appeler depuis n'importe quel script mais doit être attaché à un gameObject de la scène
  public class PlatformManager 
  {
    //Tableau de platform visible depuis l'éditeur
    [SerializeField] private GameObject[] platforms;
    
    [SerializeField] private float speed = 1f;
    [SerializeField] private float scale = 1f;
    
    public static PlatformManager Instance;
    
    private void Awake()
    {
      if (Instance == null)
        Instance = this;
    }
    
    //Appeler depuis une autre classe enfaisant PlatformManager.Instance.HideAllPlatform();
    public void HideAllPlatform()
    {
      for(var i = 0; i < platforms.length; i++) 
      {
        platforms[i].SetActive(false);
      }
    }
  }
```

## Liens utiles

### Blender et 3d en générale
Flipped Normals : https://www.youtube.com/user/FlippedNormalsTuts/featured


### Pour les débutants en Unity
Brackeys : https://www.youtube.com/user/Brackeys/featured
Sykoo : https://www.youtube.com/user/SykooTV
Mix and Jam : https://www.youtube.com/channel/UCLyVUwlB_Hahir_VsKkGPIA/featured

### Pour les avancés en Unity
Jason Weimann : https://www.youtube.com/channel/UCX_b3NNQN5bzExm-22-NVVg
Freya Holmer : https://www.youtube.com/user/Acegikm0
Sebastien Lague : https://www.youtube.com/user/Cercopithecan

### Pour réussir à déchiffrer les symboles incompréhensibles en maths
https://github.com/Jam3/math-as-code

### Si vous avez des questions sur le cours
https://twitter.com/anthelme_dumont
