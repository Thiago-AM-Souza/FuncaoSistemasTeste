let beneficiarios = [];

$(document).ready(function () {
    $('#AddBenefCpf').mask('000.000.000-00');

    $('#btnAddBenef').click(function (e) {

        var cpf = $('#AddBenefCpf').val().trim();
        var nome = $('#AddBenefNome').val().trim();

        if (!cpf || !nome) {
            return;
        }

        e.preventDefault();

        var exists = beneficiarios.some(b => b.cpf === cpf);
        if (exists) {
            ModalDialog('Erro', 'Este CPF ja foi incluido.');
        } else {
            var beneficiario = { cpf, nome };
            beneficiarios.push(beneficiario);
            renderBeneficiarios();
            $('#AddBenefCpf').val('');
            $('#AddBenefNome').val('');
        }
    });

    $('#gridBeneficiarios').on('click', '.btn-remover', function () {
        var cpf = $(this).closest('tr').find('td:first').text().trim();
        beneficiarios = beneficiarios.filter(b => b.cpf !== cpf);
        renderBeneficiarios();
    });
});

function renderBeneficiarios() {
    $('#gridBeneficiarios tbody').empty();
    beneficiarios.forEach(b => {
        var row = `
            <tr>
                <td>${b.cpf}</td>
                <td>${b.nome}</td>
                <td>
                    <button class="btn btn-danger btn-remover">Remover</button>
                </td>
            </tr>
        `;
        $('#gridBeneficiarios tbody').append(row);
    });
}

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}
