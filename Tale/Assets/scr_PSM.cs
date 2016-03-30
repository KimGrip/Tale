using UnityEngine;
using System.Collections;

public class scr_playerStateManager : MonoBehaviour {

    public enum Playerstate
    {
        state_airborne,
        state_grounded,
    }
    public enum RopeState
    {
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
}
