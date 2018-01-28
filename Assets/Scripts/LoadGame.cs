using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

class LoadGame : MonoBehaviour
{
    public void load(int level)
    {
        SceneManager.LoadScene(level);
    }
}
