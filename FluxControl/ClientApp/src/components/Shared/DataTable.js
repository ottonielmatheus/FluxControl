import React, { Component } from 'react';
import styled from 'styled-components';

const Table = styled.table`
  border-top: thin solid #707070;
  border-left: thin solid #707070;
  border-right: thin solid #707070;
  
  th {
    border-left: thin solid #707070;
  }
  th:first-child {
    border-left: none;
  }
`

const TableRowStyled = styled.tr`
  background-color: ${props => props.backgroundColor || '#FFFFFF'};
  border-bottom: thin solid #707070;
  cursor: pointer;
  transition: background-color 0.5s;

  &:hover {
    ${props => !props.header && "background-color: #ECECEC"};
  }
`

const TableHeader = styled.th`
  color: #4B4B4B;
  font-size: 20px;
  font-weight: bold;
  padding: 20px 10px;
`

const TableColumn = styled.td`
  color: #313131;
  font-size: 20px;
  padding: 20px 10px;
`

const TableDetail = styled.td.attrs(props => ({
  colSpan: props.colspan || "1",
}))``
const TableDetailDiv = styled.div`
  background-color: ${props => props.backgroundColor || '#ECECEC'};
  height: 100px;
  visibility: ${props => props.visible === 'true' ? 'visible' : 'hidden'};
  max-height: ${props => props.visible === 'true' ? '100px' : '0'};
  overflow: hidden;
  transition: max-height 0.5s, visibility 0.5s;
  border-bottom: thin solid #707070;
  ${props => props.flexright && 
  'display: flex;' +
  'justify-content: flex-end;' +
  'align-items: center;'
  }
`

class TableRow extends Component {
  constructor(props) {
    super(props);

    this.state = {
      showDetail: 'false'
    }

    this.handleDetail = this.handleDetail.bind(this);
  }

  handleDetail() {
    let show = 'true';
    if (this.state.showDetail === 'true') {
      show = 'false';
    }
    this.setState({
      showDetail: show
    })
  }

  render() {
    return (
      <React.Fragment>
        <TableRowStyled header={this.props.header} onClick={this.handleDetail}>
          {this.props.children}
        </TableRowStyled>
        {this.props.haveDetail === true &&
          <tr>
          <TableDetail colspan={this.props.colSpan}>
            <TableDetailDiv visible={this.state.showDetail} flexright={this.props.flexright}>
                {this.props.detail}
              </TableDetailDiv> 
            </TableDetail>
          </tr>
        }
      </React.Fragment>
    );
  } 
}

export class DataTable extends Component {

  render() {
    const data = this.props.data;

    let existingColumns = [];
    const Columns = data.columns.map((column) => {
      if (!existingColumns.some(obj => obj.field === column.field)){
        existingColumns.push(column);

        return (
          <TableHeader key={column.field} id={column.field} width={column.width}>{column.label}</TableHeader>
        );
      }

      return null;
    });

    let Rows = [];
    let indexRow = 0;

    data.rows.forEach((row) => {
      let indexCol = 0;
      let rowSize = 0;
      indexRow++;

      let ColumnsOfARow = data.columns.map((column) => {
        indexCol++;
        rowSize++;
        if (row[column.field]) {
          let field = row[column.field];
          return <TableColumn key={`col-${row.key}-${indexCol}`} className={column.field}>{field}</TableColumn>
        }

        return null;
      })

      Rows.push(
        <TableRow
          key={`row-${data.id}-${indexRow}`}
          colSpan={rowSize}
          haveDetail={data.options.detail}
          detail={row.detail}
          flexright={row.flexright}
        >
          {ColumnsOfARow}
        </TableRow>
      )
    })

    return (
      <Table>
        <thead>
          <TableRow header>
            {Columns}
          </TableRow>
        </thead>
        <tbody>
          {Rows}
        </tbody>
      </Table>
    );
  }
}