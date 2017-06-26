public interface IGameObjectState {
	
    //코루틴 사용하기 위한 메소드들 집합
    void Update();
	void FixedUpdate();
	void LateUpdate();
    
}