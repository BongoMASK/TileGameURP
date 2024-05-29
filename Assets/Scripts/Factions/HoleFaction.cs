public class HoleFaction : Faction
{
    private void Update() {
        if(parentTile != null) {
            parentTile.Die(); 
            Destroy(gameObject, 2);
        }
    }
}
