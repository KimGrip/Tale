using UnityEngine;
using System.Collections;

public class scr_PSM : MonoBehaviour {

    public enum Playerstate
    {
        state_respawning,
        state_airborne,
        state_grounded,
    }
    public enum RopeState
    {
        ropestate_none,
        ropestate_hanging,
        ropestate_climbing,
        ropestate_skimming,
        ropestate_swinging,
        ropestate_pulling,
    }
    public enum Equipstate
    {
        equip_none,
        equip_rope,
        equip_bow,
        equip_other,
    }
    public enum PlayerPose 
    {
        pose_standing,
        pose_crouching,
        pose_running,
        pose_jogging,
        pose_climbing,
        pose_idle,
    }

    Playerstate m_playerState;
    Equipstate m_equipState;
    RopeState m_ropeState;
    PlayerPose m_playerPose;

    public Playerstate GetPlayerState(){
        return m_playerState;
    }
    public Equipstate GetEquipState(){
        return m_equipState;
    }
    public RopeState GetRopeState(){
        return m_ropeState;
    }
    public PlayerPose GetPlayerPose(){
        return m_playerPose;
    }

    public void SetPlayerState(Playerstate p_state){
        m_playerState = p_state;
    }
    public void SetEquipState(Equipstate p_state){
        m_equipState = p_state;
    }
    public void SetRopeState(RopeState p_state){
        m_ropeState = p_state;
    }
    public void SetPlayerPose(PlayerPose p_pose){
        m_playerPose = p_pose;
    }
  public void OnGUI()
    {
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height),
            "Current Rope State     "+m_ropeState+"\n"+
            "Current Player State   " + m_playerState +"\n"+
            "Current Player Pose    " +m_playerPose+"\n"+
            "Current Equip State    " +m_equipState);
        //print(m_PSM.GetRopeState());
        //print(m_PSM.GetPlayerState());
        //print(m_PSM.GetPlayerPose());
        //print(m_PSM.GetEquipState());
    }
}
