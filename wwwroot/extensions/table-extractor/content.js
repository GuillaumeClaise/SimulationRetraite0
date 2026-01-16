function extractTables() {
  const tables = document.querySelectorAll('table');
  const extractedData = [];

  tables.forEach((table, index) => {
    const tableData = {
      index: index + 1,
      headers: [],
      rows: []
    };

    const thead = table.querySelector('thead');
    const tbody = table.querySelector('tbody');
    const rows = tbody ? tbody.querySelectorAll('tr') : table.querySelectorAll('tr');

    if (thead) {
      const headerCells = thead.querySelectorAll('th');
      tableData.headers = Array.from(headerCells).map(cell => cell.textContent.trim());
    } else {
      const firstRow = table.querySelector('tr');
      if (firstRow) {
        const firstRowCells = firstRow.querySelectorAll('th, td');
        const hasHeaders = firstRow.querySelectorAll('th').length > 0;

        if (hasHeaders) {
          tableData.headers = Array.from(firstRowCells).map(cell => cell.textContent.trim());
        }
      }
    }

    const dataRows = Array.from(rows).filter(row => {
      if (!thead && row === table.querySelector('tr') && row.querySelectorAll('th').length > 0) {
        return false;
      }
      return true;
    });

    dataRows.forEach(row => {
      const cells = row.querySelectorAll('td, th');
      const rowData = Array.from(cells).map(cell => cell.textContent.trim());

      if (rowData.some(cell => cell !== '')) {
        tableData.rows.push(rowData);
      }
    });

    if (tableData.rows.length > 0 || tableData.headers.length > 0) {
      extractedData.push(tableData);
    }
  });

  return extractedData;
}

extractTables();
