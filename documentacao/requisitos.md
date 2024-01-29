# Clínica Alo Doutor


## Histórico da Clínica

A clímica Alô Doutor provê um serviço gratuito de consultas médicas para a população utilizando o sistema de atendimento presencial. 

A  clínica foi fundada em 2010 e desde então vem atendendo a população de forma gratuita. A clínica conta com médicos voluntários que atendem a população de segunda a sexta das 8h às 18h. 

Os médicos realizam consultas humanizadas de 1 hora e tem um intervalo de almoço das 12:00 as 14:00.

Atualmente todo o trabalho da clínica é feito de forma manual em fichas de papel, porém a clínica está buscando modernizar o seu sistema de marcação de ocnsultas para melhorar a experiência do paciente e do médico.

Nesse momento foi solicitado que a informatização fosse realizada sem melhorias no processo atual. 

### DDD

#### Domain Storytelling

O time de desenvolvimento conversou com o responsável administrativo pela clínica e identificou os seguintes pontos:

![Cadastro do Médico](./imagens/01-CadastroMedico.png)
</br>
</br>
![Cadastro do Paciente](./imagens/02-CadastroPaciente.png)
</br>
</br>
![Agendamento Consulta](./imagens/03-AgendamentoConsulta.png)




#### Domínios e Contextos Delimitados Identificados

##### Domínios

![Domínios Identificados](./imagens/dominiosAloDoutor.png)



#### Contextos Delimitados

![Mapa de Contextos Delimitados](./imagens/mapaContextos.png)


### Critérios de Aceite

- Cadastro de Médico 
    - As seguintes informações são obrigatórias no cadastro:
        - Nome
        - CPF
        - CRM
        - Telefone
        - Endereço
        - Estado
        - CEP
    - A especialidade é opcional
    - Um médico pode atender em mais de uma especialidade
    - Não podem haver dois médicos com o mesmo CRM
    - Não podem haver dois médicos com o mesmo CPF
    

</br>

- Cadastro do Paciente
    - As seguintes informações são obrigatórias:
        - Nome
        - CPF
        - Idade
        - Telefone
        - Endereço
        - Estado
        - CEP

    - Não podem haver dois pacientes com o mesmo CPF
    
<br>



- Marcação de Consultas
    - O Paciente e o Médico já devem estar cadastrados
    - Só podem ser considerados médicos que tem especialidade associada
    - As consultas só podem ser agendadas para os seguites dias:
        - Segunda-feira a Sexta-feira das 08h às 12h
        - Segunda-feira a Sexta-feira das 14h às 18h
    - Cada consulta tem duração de 1 hora obrigatoriamente
    - Não há necessidade de considerar feriados
    - As consultas devem ser agendadas com um mínimo de 2 horas de antecedência
    - O Paciente não pode marcar duas consultas no mesmo dia e horário
    - A consulta pode ser desativada pelos usuários do sistema
   
<br>

- Controle de Acesso
    - Todas as funcionalizadas devem ser executadas por um Funcionário previamente cadastrado no sistema
    - O sistema pode ter somente um tipo de usuário que terá permissão total nas funcionalidades
    - Se o usuário não estiver autenticado, ele não pode acessar as funcionalidades do sistema



*[Voltar](../README.md)*