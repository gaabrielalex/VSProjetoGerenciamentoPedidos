
CREATE TABLE cliente (
    id_cliente INT IDENTITY(1, 1) PRIMARY KEY,
    nome varchar(100) NOT NULL,
    cpf char(11) UNIQUE,
    telefone varchar(14)
);

CREATE TABLE metodo_pagto (
    id_metodo_pagto INT IDENTITY(1, 1) PRIMARY KEY,
    descricao varchar(50) NOT NULL,
);

CREATE TABLE produto (
    id_produto INT IDENTITY(1, 1) PRIMARY KEY,
    descricao varchar(200) NOT NULL,
    vlr_unitario numeric(8,2)
);

CREATE TABLE pedido (
    id_pedido INT IDENTITY(1, 1) PRIMARY KEY,
    vlr_total_pedido numeric(9,2),
    desconto numeric(8,2),
    dt_hr_pedido datetime,
    status_pedido char(1) CHECK (status_pedido IN ('A', 'S', 'E', 'C')),
    observacoes varchar(200),
    id_metodo_pagto int,
    id_cliente int,
    FOREIGN KEY(id_metodo_pagto) REFERENCES metodo_pagto (id_metodo_pagto),
    FOREIGN KEY(id_cliente) REFERENCES cliente (id_cliente)
);

CREATE TABLE item_pedido (
    id_item_pedido INT IDENTITY(1, 1) PRIMARY KEY,
    qtde int,
    vlr_total_item numeric(6,2),
    id_pedido int,
    id_produto int,
    FOREIGN KEY(id_pedido) REFERENCES pedido (id_pedido),
    FOREIGN KEY(id_produto) REFERENCES produto (id_produto)
);
