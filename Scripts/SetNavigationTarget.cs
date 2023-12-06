using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SetNavigationTarget : MonoBehaviour
{
    // Variáveis de instância
    [SerializeField]
    private TMP_Dropdown navigationTargetDropDown; // Dropdown para seleção de alvo de navegação
    [SerializeField]
    private List<Target> navigationTargetObjects = new List<Target>(); // Lista de objetos de alvo de navegação
    [SerializeField]
    private Slider navigationYOffset; // Slider para ajustar a altura da linha de navegação

    private NavMeshPath path; // Caminho de navegação calculado
    private LineRenderer line; // LineRenderer para exibir o caminho
    private Vector3 targetPosition = Vector3.zero; // Posição do alvo de navegação
    private int currentFloor = 1; // Andar atual
    private bool lineToggle = false; // Controle de visibilidade da linha de navegação

    // Método chamado no início
    private void Start()
    {
        path = new NavMeshPath(); // Inicializa o caminho de navegação
        line = transform.GetComponent<LineRenderer>(); // Obtém o componente LineRenderer
        line.enabled = lineToggle; // Define a visibilidade da linha de navegação
    }

    // Método chamado a cada quadro de atualização
    private void Update()
    {
        // Verifica se a linha de navegação está ativada e a posição do alvo não é nula
        if (lineToggle && targetPosition != Vector3.zero)
        {
            // Calcula o caminho de navegação
            NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path);

            // Define o número de posições da linha de navegação com base no número de cantos do caminho
            line.positionCount = path.corners.Length;

            // Adiciona um deslocamento vertical às posições do caminho e atualiza a linha
            Vector3[] calculatedPathAndOffset = AddLineOffset();
            line.SetPositions(calculatedPathAndOffset);
        }
    }

    // Método para definir o alvo de navegação atual com base no valor selecionado no dropdown
    public void SetCurrentNavigationTarget(int selectedValue)
    {
        targetPosition = Vector3.zero; // Redefine a posição do alvo

        // Obtém o texto selecionado no dropdown
        string selectedText = navigationTargetDropDown.options[selectedValue].text;

        // Procura o objeto de alvo com o nome correspondente ao texto selecionado
        Target currentTarget = navigationTargetObjects.Find(x => x.Name.ToLower().Equals(selectedText.ToLower()));

        if (currentTarget != null)
        {
            // Se o objeto de alvo for encontrado e a linha de navegação estiver desativada, ativa-a
            if (!line.enabled)
            {
                ToggleVisibility();
            }

            // Define a posição do alvo de navegação
            targetPosition = currentTarget.PositionObject.transform.position;
        }
    }

    // Método para alternar a visibilidade da linha de navegação
    public void ToggleVisibility()
    {
        lineToggle = !lineToggle; // Inverte o estado de visibilidade da linha
        line.enabled = lineToggle; // Atualiza a visibilidade da linha
    }

    // Método para alterar o andar ativo
    public void ChangeActiveFloor(int floorNumber) {
        currentFloor = floorNumber; // Define o novo andar ativo
        SetNavigationTargetDropDownOptions(currentFloor); // Atualiza as opções do dropdown
    }

    // Método para adicionar um deslocamento vertical às posições do caminho
    private Vector3[] AddLineOffset()
    {
        if (navigationYOffset.value == 0)
        {
            return path.corners; // Retorna as posições do caminho sem deslocamento
        }

        // Cria um novo array de posições com deslocamento vertical
        Vector3[] calculatedLine = new Vector3[path.corners.Length];
        for (int i = 0; i < path.corners.Length; i++)
        {
            calculatedLine[i] = path.corners[i] + new Vector3(0, navigationYOffset.value, 0);
        }
        return calculatedLine;
    }

    // Método para definir as opções do dropdown de alvos de navegação com base no andar atual
    private void SetNavigationTargetDropDownOptions(int floorNumber)
    {
        navigationTargetDropDown.ClearOptions(); // Limpa as opções do dropdown
        navigationTargetDropDown.value = 0; // Define o valor padrão do dropdown

        // Se a linha de navegação estiver ativada, a desativa
        if (line.enabled)
        {
            ToggleVisibility();
        }
        
        if (floorNumber == 1) // INFORMATICA
        {
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Hardware"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Pesquisa e Extensão"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Aula"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala dos Professores"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Laboratório 1"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Laboratório 2"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Laboratório 3"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Laboratório 4"));

        }
        if (floorNumber == 2) // SALÃO SOCIAL
        {
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Salao Social"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Fisica"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("CPPD/CIS"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("PIBID"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Xerox"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Depósito"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Banheiro Feminino"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Banheiro Masculino"));
        }
        if (floorNumber == 3) // AGRIMENSURA
        {
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Coordenação Agrimensura"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Coordenação AeO"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Aula 1"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Aula 2"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Aula 3"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Laboratorio de Informatica"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Equipamentos"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Reunião"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Impressão"));
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
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Recepção"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Licitação"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Auditoria Interno"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Departamento de Admin"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Coordenação Geral"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("SAPCC"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Cozinha"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Sala de Reuniao"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Banheiro Masculino"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Banheiro Feminino"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Coordenação de Superior Escolar"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Departamento Educacional"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Secretaria"));
        }
    }
}
