using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = UnityEngine.Random;

public class LHScript : MonoBehaviour
{
    public KMAudio Audio;
    public KMBombModule module;
    public List<KMSelectable> buttons;
    public GameObject[] solids;
    public GameObject sphere;
    public Renderer[] rings;
    public Material[] io;
    public TextMesh[] btexts;

    private readonly string[] snames = new string[4] { "Tetrahedron", "Hexahedron", "Octahedron", "Dodecahedron" };
    private float[] xaxis = new float[4] { -0.0477f, -0.016f, 0.016f, 0.0477f };
    private int[] ord = new int[4] { 0, 1, 2, 3 };
    private int[][][][][] sol = new int[5][][][][]
    {
        new int[4][][][]
        {
            new int[4][][]{ new int[4][]{ new int[4]{ 1, 2, 3, 4}, new int[4]{ 2, 3, 4, 1}, new int[4]{ 3, 4, 1, 2}, new int[4]{ 4, 1, 2, 3} }, new int[4][]{ new int[4]{ 2, 3, 4, 1}, new int[4]{ 3, 4, 1, 2}, new int[4]{ 4, 1, 2, 3}, new int[4]{ 1, 2, 3, 4} }, new int[4][]{ new int[4]{ 3, 4, 1, 2}, new int[4]{ 4, 1, 2, 3}, new int[4]{ 1, 2, 3, 4}, new int[4]{ 2, 3, 4, 1} }, new int[4][]{ new int[4]{ 4, 1, 2, 3}, new int[4]{ 1, 2, 3, 4}, new int[4]{ 2, 3, 4, 1}, new int[4]{ 3, 4, 1, 2} } },
            new int[4][][]{ new int[4][]{ new int[4]{ 2, 3, 4, 1}, new int[4]{ 3, 4, 1, 2}, new int[4]{ 4, 1, 2, 3}, new int[4]{ 1, 2, 3, 4} }, new int[4][]{ new int[4]{ 3, 4, 1, 2}, new int[4]{ 4, 1, 2, 3}, new int[4]{ 1, 2, 3, 4}, new int[4]{ 2, 3, 4, 1} }, new int[4][]{ new int[4]{ 4, 1, 2, 3}, new int[4]{ 1, 2, 3, 4}, new int[4]{ 2, 3, 4, 1}, new int[4]{ 3, 4, 1, 2} }, new int[4][]{ new int[4]{ 1, 2, 3, 4}, new int[4]{ 2, 3, 4, 1}, new int[4]{ 3, 4, 1, 2}, new int[4]{ 4, 1, 2, 3} } },
            new int[4][][]{ new int[4][]{ new int[4]{ 3, 4, 1, 2}, new int[4]{ 4, 1, 2, 3}, new int[4]{ 1, 2, 3, 4}, new int[4]{ 2, 3, 4, 1} }, new int[4][]{ new int[4]{ 4, 1, 2, 3}, new int[4]{ 1, 2, 3, 4}, new int[4]{ 2, 3, 4, 1}, new int[4]{ 3, 4, 1, 2} }, new int[4][]{ new int[4]{ 1, 2, 3, 4}, new int[4]{ 2, 3, 4, 1}, new int[4]{ 3, 4, 1, 2}, new int[4]{ 4, 1, 2, 3} }, new int[4][]{ new int[4]{ 2, 3, 4, 1}, new int[4]{ 3, 4, 1, 2}, new int[4]{ 4, 1, 2, 3}, new int[4]{ 1, 2, 3, 4} } },
            new int[4][][]{ new int[4][]{ new int[4]{ 4, 1, 2, 3}, new int[4]{ 1, 2, 3, 4}, new int[4]{ 2, 3, 4, 1}, new int[4]{ 3, 4, 1, 2} }, new int[4][]{ new int[4]{ 1, 2, 3, 4}, new int[4]{ 2, 3, 4, 1}, new int[4]{ 3, 4, 1, 2}, new int[4]{ 4, 1, 2, 3} }, new int[4][]{ new int[4]{ 2, 3, 4, 1}, new int[4]{ 3, 4, 1, 2}, new int[4]{ 4, 1, 2, 3}, new int[4]{ 1, 2, 3, 4} }, new int[4][]{ new int[4]{ 3, 4, 1, 2}, new int[4]{ 4, 1, 2, 3}, new int[4]{ 1, 2, 3, 4}, new int[4]{ 2, 3, 4, 1} } }
        },
    new int[4][][][]{ new int[4][][] { new int[4][] { new int[4] , new int[4] , new int[4] , new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] } },  new int[4][][] { new int[4][] { new int[4] , new int[4] , new int[4] , new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] } },  new int[4][][] { new int[4][] { new int[4] , new int[4] , new int[4] , new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] } },  new int[4][][] { new int[4][] { new int[4] , new int[4] , new int[4] , new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] } } },
    new int[4][][][]{ new int[4][][] { new int[4][], new int[4][], new int[4][], new int[4][] },  new int[4][][] { new int[4][], new int[4][], new int[4][], new int[4][]},  new int[4][][] { new int[4][], new int[4][], new int[4][], new int[4][]},  new int[4][][] { new int[4][], new int[4][], new int[4][], new int[4][] } },
    new int[4][][][]{ new int[4][][], new int[4][][],  new int[4][][], new int[4][][] },
    new int[4][][][]};
    private int[][][][] clues = new int[4][][][] { new int[4][][] { new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] } }, new int[4][][] { new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] } }, new int[4][][] { new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] } }, new int[4][][] { new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] } } };
    private int[] iter;
    private int zeros;
    private bool prog;
    private int[] pos = new int[4];

    private static int moduleIDCounter;
    private int moduleID;
    private bool moduleSolved;

    private void Awake()
    {
        moduleID = moduleIDCounter++;
        ord = ord.Shuffle();
        for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++) for (int k = 0; k < 4; k++) for (int l = 0; l < 4; l++) sol[1][i][j][k][ord[l]] = sol[0][i][j][k][l];
        ord = ord.Shuffle();
        for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++) for (int k = 0; k < 4; k++) sol[2][i][j][ord[k]] = sol[1][i][j][k];
        ord = ord.Shuffle();
        for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++) sol[3][i][ord[j]] = sol[2][i][j];
        ord = ord.Shuffle();
        for (int i = 0; i < 4; i++) sol[4][ord[i]] = sol[3][i];
        for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++) for (int k = 0; k < 4; k++) for (int l = 0; l < 4; l++) clues[i][j][k][l] = sol[4][i][j][k][l];
        List<int> removeorder = Enumerable.Range(0, 256).ToArray().Shuffle().ToList();
        int index = 0;
        while (index < removeorder.Count())
        {
            int r = removeorder[index];
            int recover = clues[r / 64][(r / 16) % 4][(r / 4) % 4][r % 4];
            clues[r / 64][(r / 16) % 4][(r / 4) % 4][r % 4] = 0;
            iter = new int[2] { 256, 256 };
            if (Test(clues))
            {
                removeorder.RemoveAt(index);
                zeros++;
            }
            else
            {
                clues[r / 64][(r / 16) % 4][(r / 4) % 4][r % 4] = recover;
                index++;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            int r = Random.Range(0, 256);
            while (clues[r / 64][(r / 16) % 4][(r / 4) % 4][r % 4] != 0)
                r = Random.Range(0, 256);
            clues[r / 64][(r / 16) % 4][(r / 4) % 4][r % 4] = sol[4][r / 64][(r / 16) % 4][(r / 4) % 4][r % 4];
        }
        Debug.LogFormat("[Latin Hypercube #{0}] The given shapes are:\n[Latin Hypercube #{0}] {1}", moduleID, string.Join(Environment.NewLine + "[Latin Hypercube #" + moduleID + "] " + Environment.NewLine + "[Latin Hypercube #" + moduleID + "] ", clues.Select(w => string.Join(Environment.NewLine + "[Latin Hypercube #" + moduleID + "] ", w.Select(z => string.Join("   ", z.Select(y => string.Join(" ", y.Select(x => "#THOD"[x].ToString()).ToArray())).ToArray())).ToArray())).ToArray()));
        Debug.LogFormat("[Latin Hypercube #{0}] Solution:\n[Latin Hypercube #{0}] {1}", moduleID, string.Join(Environment.NewLine + "[Latin Hypercube #" + moduleID + "] " + Environment.NewLine + "[Latin Hypercube #" + moduleID + "] ", sol[4].Select(w => string.Join(Environment.NewLine + "[Latin Hypercube #" + moduleID + "] ", w.Select(z => string.Join("   ", z.Select(y => string.Join(" ", y.Select(x => "#THOD"[x].ToString()).ToArray())).ToArray())).ToArray())).ToArray()));
        Fill(0);
        for (int i = 0; i < 4; i++)
        {
            Vector3 v = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            for (int j = 0; j < 16; j++)
                StartCoroutine(Spin((16 * i) + j, v));
            StartCoroutine(Spin(64 + i, v));
        }
        Occupy(pos);
        foreach (KMSelectable button in buttons)
        {
            int b = buttons.IndexOf(button);
            button.OnInteract += delegate ()
            {
                if (!moduleSolved)
                {
                    if (b > 3)
                    {
                        if (clues[pos[3]][pos[2]][pos[1]][pos[0]] == 0)
                        {
                            button.AddInteractionPunch(0.4f);
                            if (b - 3 == sol[4][pos[3]][pos[2]][pos[1]][pos[0]])
                            {
                                Audio.PlaySoundAtTransform("Place", sphere.transform);
                                Debug.LogFormat("[Latin Hypercube #{0}] Placed {1} at ({2},{3},{4},{5}).", moduleID, snames[b - 4], pos[0], pos[1], pos[2], pos[3]);
                                clues[pos[3]][pos[2]][pos[1]][pos[0]] = b - 3;
                                sphere.SetActive(false);
                                rings[b - 4].material = io[0];
                                solids[((b - 4) * 16) + (pos[1] * 4) + pos[2]].SetActive(true);
                                zeros--;
                                if (zeros < 128)
                                {
                                    moduleSolved = true;
                                    module.HandlePass();
                                    Audio.PlaySoundAtTransform("Solve", transform);
                                    foreach (TextMesh t in btexts)
                                        t.text = "";
                                    foreach (Renderer r in rings)
                                        r.material = io[0];
                                    foreach (GameObject g in solids)
                                        g.SetActive(true);
                                    StartCoroutine(SolveAnim());
                                }
                                else
                                {
                                    if (prog)
                                        StopCoroutine("Prog");
                                    StartCoroutine("Prog");
                                }
                            }
                            else
                            {
                                Debug.LogFormat("[Latin Hypercube #{0}] {1} does not belong at ({2},{3},{4},{5}).", moduleID, snames[b - 4], pos[0], pos[1], pos[2], pos[3]);
                                module.HandleStrike();
                            }
                        }
                    }
                    else
                    {
                        button.AddInteractionPunch(0.1f);
                        pos[b]++;
                        pos[b] %= 4;
                        if(!prog)
                        btexts[b].text = pos[b].ToString();
                        Occupy(pos);
                        if (b == 3)
                            Fill(pos[3]);
                        else
                            sphere.transform.localPosition = new Vector3(xaxis[pos[0]], new float[] { -0.0442f, -0.0148f, 0.0172f, 0.047f }[pos[1]], new float[] { 0.048f, 0.016f, -0.016f, -0.048f }[pos[2]]);
                        Audio.PlaySoundAtTransform("Scroll" + pos[b], sphere.transform);
                    }
                }
                return false;
            };
        }
    }

    private void Fill(int w)
    {
        List<int>[][] solh = Enumerable.Range(0, 4).Select(i => Enumerable.Range(0, 4).Select(j => sol[4][w][i][j].ToList()).ToArray()).ToArray();
        List<int>[][] cube = Enumerable.Range(0, 4).Select(i => Enumerable.Range(0, 4).Select(j => clues[w][i][j].ToList()).ToArray()).ToArray();
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                for (int k = 0; k < 4; k++)
                {
                    GameObject s = solids[(k * 16) + (j * 4) + i];
                    s.SetActive(true);
                    Vector3 t = s.transform.localPosition;
                    s.transform.localPosition = new Vector3(xaxis[solh[i][j].IndexOf(k + 1)], t.y, t.z);
                    if (cube[i][j].IndexOf(k + 1) < 0)
                        s.SetActive(false);
                }
    }

    private IEnumerator Spin(int s, Vector3 v)
    {
        while (true)
        {
            solids[s].transform.Rotate(v, 3);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private bool Test(int[][][][] x)
    {
        int[][][][] newcube = new int[4][][][] { new int[4][][] { new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] } }, new int[4][][] { new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] } }, new int[4][][] { new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] } }, new int[4][][] { new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] }, new int[4][] { new int[4], new int[4], new int[4], new int[4] } } };
        for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++) for (int k = 0; k < 4; k++) for (int l = 0; l < 4; l++) newcube[i][j][k][l] = x[i][j][k][l];
        iter[1] = iter[0];
        if (iter[1] == 0)
            return true;
        iter[0] = 0;
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                for (int k = 0; k < 4; k++)
                    for (int l = 0; l < 4; l++)
                        if (x[i][j][k][l] == 0)
                        {
                            int[] o = new int[4] { 1, 2, 3, 4 };
                            int[] d = new int[12] { x[i][j][k][(l + 1) % 4], x[i][j][k][(l + 2) % 4], x[i][j][k][(l + 3) % 4], x[i][j][(k + 1) % 4][l], x[i][j][(k + 2) % 4][l], x[i][j][(k + 3) % 4][l], x[i][(j + 1) % 4][k][l], x[i][(j + 2) % 4][k][l], x[i][(j + 3) % 4][k][l], x[(i + 1) % 4][j][k][l], x[(i + 2) % 4][j][k][l], x[(i + 3) % 4][j][k][l] };
                            o = o.Except(d).ToArray();
                            if (o.Count() < 2)
                                newcube[i][j][k][l] = o[0];
                            else
                                iter[0]++;
                        }
        if (iter[0] == iter[1])
            return false;
        return Test(newcube);
    }

    private void Occupy(int[] p)
    {
        int s = clues[p[3]][p[2]][p[1]][p[0]];
        foreach (Renderer r in rings)
            r.material = io[1];
        if (s == 0)
            sphere.SetActive(true);
        else
        {
            sphere.SetActive(false);
            rings[s - 1].material = io[0];
        }
    }

    private IEnumerator Prog()
    {
        prog = true;
        int p = zeros - 127;
        btexts[0].text = "\u2713";
        btexts[1].text = (p / 100).ToString();
        btexts[2].text = ((p / 10) % 10).ToString();
        btexts[3].text = (p % 10).ToString();
        yield return new WaitForSeconds(0.5f);
        prog = false;
        if(!moduleSolved)
            for (int i = 0; i < 4; i++)
                btexts[i].text = pos[i].ToString();
    }

    private IEnumerator SolveAnim()
    {
        int w = pos[3];
        while (true)
        {
            List<int>[][] solh = Enumerable.Range(0, 4).Select(i => Enumerable.Range(0, 4).Select(j => sol[4][w][i][j].ToList()).ToArray()).ToArray();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    for (int k = 0; k < 4; k++)
                    {
                        GameObject s = solids[(k * 16) + (j * 4) + i];
                        s.SetActive(true);
                        Vector3 t = s.transform.localPosition;
                        s.transform.localPosition = new Vector3(xaxis[solh[i][j].IndexOf(k + 1)], t.y, t.z);
                    }
            yield return new WaitForSeconds(2);
            w++;
            w %= 4;
        }
    }

    //twitch plays
    private float _tpSpeed = 0.1f;
#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} x/y/z/w [Presses the specified coordinate button] | !{0} t/h/o/d [Presses the specified solid button] | Commands may be chained, for example '!{0} xxywh' | !{0} setspeed 0.2 [Set a press speed between 0 and 1 seconds.]";
#pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        var parameters = command.ToLowerInvariant().Split(' ');
        var m = Regex.Match(parameters[0], @"^\s*setspeed\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        if (m.Success)
        {
            if (parameters.Length != 2)
                yield break;
            float tempSpeed;
            if (!float.TryParse(parameters[1], out tempSpeed) || tempSpeed <= 0 || tempSpeed > 1)
            {
                yield return "sendtochaterror " + parameters[1] + " is not a valid speed! Press speed must be between 0 and 1 seconds.";
                yield break;
            }
            yield return null;
            _tpSpeed = tempSpeed;
            yield return "sendtochat Latin Hypercube's press speed has been set to " + parameters[1];
            yield break;
        }
        var chars = "xyzwthod ".ToCharArray();
        var list = new List<int>();
        for (int i = 0; i < command.Length; i++)
        {
            int ix = Array.IndexOf(chars, command[i]);
            if (ix == -1)
                yield break;
            if (ix == 8)
                continue;
            list.Add(ix);
        }
        yield return null;
        for (int i = 0; i < list.Count; i++)
        {
            buttons[list[i]].OnInteract();
            yield return new WaitForSeconds(_tpSpeed);
        }
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        for (int w = 0; w < 4; w++)
        {
            for (int z = 0; z < 4; z++)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        if (clues[pos[3]][pos[2]][pos[1]][pos[0]] == 0)
                        {
                            buttons[sol[4][pos[3]][pos[2]][pos[1]][pos[0]] + 3].OnInteract();
                            yield return new WaitForSeconds(0.1f);
                            if (moduleSolved) yield break;
                        }
                        buttons[0].OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    buttons[1].OnInteract();
                    yield return new WaitForSeconds(0.1f);
                }
                buttons[2].OnInteract();
                yield return new WaitForSeconds(0.1f);
            }
            buttons[3].OnInteract();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
