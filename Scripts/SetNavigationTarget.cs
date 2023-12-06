using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SetNavigationTarget : MonoBehaviour
{
    // Vari�veis de inst�ncia
    [SerializeField]
    private TMP_Dropdown navigationTargetDropDown; // Dropdown para sele��o de alvo de navega��o
    [SerializeField]
    private List<Target> navigationTargetObjects = new List<Target>(); // Lista de objetos de alvo de navega��o
    [SerializeField]
    private Slider navigationYOffset; // Slider para ajustar a altura da linha de navega��o

    private NavMeshPath path; // Caminho de navega��o calculado
    private LineRenderer line; // LineRenderer para exibir o caminho
    private Vector3 targetPosition = Vector3.zero; // Posi��o do alvo de navega��o
    private int currentFloor = 1; // Andar atual
    private bool lineToggle = false; // Controle de visibilidade da linha de navega��o

    // M�todo chamado no in�cio
    private void Start()
    {
        path = new NavMeshPath(); // Inicializa o caminho de navega��o
        line = transform.GetComponent<LineRenderer>(); // Obt�m o componente LineRenderer
        line.enabled = lineToggle; // Define a visibilidade da linha de navega��o
    }

    // M�todo chamado a cada quadro de atualiza��o
    private void Update()
    {
        // Verifica se a linha de navega��o est� ativada e a posi��o do alvo n�o � nula
        if (lineToggle && targetPosition != Vector3.zero)
        {
            // Calcula o caminho de navega��o
            NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path);

            // Define o n�mero de posi��es da linha de navega��o com base no n�mero de cantos do caminho
            line.positionCount = path.corners.Length;

            // Adiciona um deslocamento vertical �s posi��es do caminho e atualiza a linha
            Vector3[] calculatedPathAndOffset = AddLineOffset();
            line.SetPositions(calculatedPathAndOffset);
        }
    }

    // M�todo para definir o alvo de navega��o atual com base no valor selecionado no dropdown
    public void SetCurrentNavigationTarget(int selectedValue)
    {
        targetPosition = Vector3.zero; // Redefine a posi��o do alvo

        // Obt�m o texto selecionado no dropdown
        string selectedText = navigationTargetDropDown.options[selectedValue].text;

        // Procura o objeto de alvo com o nome correspondente ao texto selecionado
        Target currentTarget = navigationTargetObjects.Find(x => x.Name.ToLower().Equals(selectedText.ToLower()));

        if (currentTarget != null)
        {
            // Se o objeto de alvo for encontrado e a linha de navega��o estiver desativada, ativa-a
            if (!line.enabled)
            {
                ToggleVisibility();
            }

            // Define a posi��o do alvo de navega��o
            targetPosition = currentTarget.PositionObject.transform.position;
        }
    }

    // M�todo para alternar a visibilidade da linha de navega��o
    public void ToggleVisibility()
    {
        lineToggle = !lineToggle; // Inverte o estado de visibilidade da linha
        line.enabled = lineToggle; // Atualiza a visibilidade da linha
    }

    // M�todo para alterar o andar ativo
    public void ChangeActiveFloor(int floorNumber) {
        currentFloor = floorNumber; // Define o novo andar ativo
        SetNavigationTargetDropDownOptions(currentFloor); // Atualiza as op��es do dropdown
    }

    // M�todo para adicionar um deslocamento vertical �s posi��es do caminho
    private Vector3[] AddLineOffset()
    {
        if (navigationYOffset.value == 0)
        {
            return path.corners; // Retorna as posi��es do caminho sem deslocamento
        }

        // Cria um novo array de posi��es com deslocamento vertical
        Vector3[] calculatedLine = new Vector3[path.corners.Length];
        for (int i = 0; i < path.corners.Length; i++)
        {
            calculatedLine[i] = path.corners[i] + new Vector3(0, navigationYOffset.value, 0);
        }
        return calculatedLine;
    }

    // M�todo para definir as op��es do dropdown de alvos de navega��o com base no andar atual
    private void SetNavigationTargetDropDownOptions(int floorNumber)
    {
        navigationTargetDropDown.ClearOptions(); // Limpa as op��es do dropdown
        navigationTargetDropDown.value = 0; // Define o valor padr�o do dropdown

        // Se a linha de navega��o estiver ativada, a desativa
        if (line.enabled)
        {
            ToggleVisibility();
        }
        
        if (floorNumber == 1) // INFORMATICA
        {
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Hardware"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Pesquisa e Extens�o"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Aula"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala dos Professores"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Laborat�rio 1"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Laborat�rio 2"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Laborat�rio 3"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Laborat�rio 4"));

        }
        if (floorNumber == 2) // SAL�O SOCIAL
        {
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Salao Social"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Fisica"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("CPPD/CIS"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("PIBID"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Xerox"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Dep�sito"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Banheiro Feminino"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Banheiro Masculino"));
        }
        if (floorNumber == 3) // AGRIMENSURA
        {
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Coordena��o Agrimensura"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Coordena��o AeO"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Aula 1"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Aula 2"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Aula 3"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Laboratorio de Informatica"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Equipamentos"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Reuni�o"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Impress�o"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Laboratorio de Agrimensura"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala dos Professores"));
        }
        if (floorNumber == 4) // PREDIO CIMA
        {
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("SalaDosProfessores 1"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("SalaDosProfessores 2"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala 1"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala 2"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala 3"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala 4"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala 5"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala 6"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala 7"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Arquivos"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Laboratorio 1"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala 7"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Banheiro Feminino"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Banheiro Masculino"));
        }
        if (floorNumber == 5) // PREDIO BAIXO
        {
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Servidor Geral"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Gabinete Diretor 1"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Gabinete Diretor 2"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Recep��o"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Licita��o"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Auditoria Interno"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Departamento de Admin"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Coordena��o Geral"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("SAPCC"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Cozinha"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Reuniao"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Banheiro Masculino"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Banheiro Feminino"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Coordena��o de Superior Escolar"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Departamento Educacional"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Secretaria"));
        }
    }
}
