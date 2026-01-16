let extractedTables = [];

async function extractTables() {
  const statusDiv = document.getElementById('status');
  const resultsDiv = document.getElementById('results');

  statusDiv.textContent = 'Extraction en cours...';
  statusDiv.className = 'status-inline';

  try {
    const [tab] = await chrome.tabs.query({ active: true, currentWindow: true });

    const results = await chrome.scripting.executeScript({
      target: { tabId: tab.id },
      files: ['content.js']
    });

    extractedTables = results[0].result;

    if (extractedTables && extractedTables.length > 0) {
      displayTables(extractedTables);
      statusDiv.textContent = `${extractedTables.length} tableau(x) extrait(s) avec succès!`;
      statusDiv.className = 'status-inline success';
      resultsDiv.classList.remove('hidden');
    } else {
      statusDiv.textContent = 'Aucun tableau trouvé sur cette page.';
      statusDiv.className = 'status-inline error';
      resultsDiv.classList.add('hidden');
    }
  } catch (error) {
    statusDiv.textContent = `Erreur: ${error.message}`;
    statusDiv.className = 'status-inline error';
    resultsDiv.classList.add('hidden');
  }
}

window.addEventListener('DOMContentLoaded', extractTables);

function displayTables(tables) {
  const container = document.getElementById('tablesContainer');
  const countSpan = document.getElementById('tableCount');

  countSpan.textContent = tables.length;
  container.innerHTML = '';

  tables.forEach((tableData, index) => {
    const tableDiv = document.createElement('div');
    tableDiv.className = 'table-preview';

    const title = document.createElement('h3');
    title.textContent = `Tableau ${index + 1}`;
    tableDiv.appendChild(title);

    const table = document.createElement('table');
    table.className = 'preview-table';

    if (tableData.headers.length > 0) {
      const thead = document.createElement('thead');
      const headerRow = document.createElement('tr');

      tableData.headers.forEach(header => {
        const th = document.createElement('th');
        th.textContent = header;
        headerRow.appendChild(th);
      });

      thead.appendChild(headerRow);
      table.appendChild(thead);
    }

    const tbody = document.createElement('tbody');
    const rowsToShow = tableData.rows.slice(0, 5);

    rowsToShow.forEach(row => {
      const tr = document.createElement('tr');

      row.forEach(cell => {
        const td = document.createElement('td');
        td.textContent = cell;
        tr.appendChild(td);
      });

      tbody.appendChild(tr);
    });

    table.appendChild(tbody);
    tableDiv.appendChild(table);

    if (tableData.rows.length > 5) {
      const moreText = document.createElement('p');
      moreText.className = 'more-rows';
      moreText.textContent = `... et ${tableData.rows.length - 5} ligne(s) de plus`;
      tableDiv.appendChild(moreText);
    }

    container.appendChild(tableDiv);
  });
}

document.getElementById('copyBtn').addEventListener('click', () => {
  const json = JSON.stringify(extractedTables, null, 2);
  navigator.clipboard.writeText(json).then(() => {
    const statusDiv = document.getElementById('status');
    statusDiv.textContent = 'JSON copié dans le presse-papiers!';
    statusDiv.className = 'status-inline success';
    statusDiv.classList.remove('hidden');

    setTimeout(() => {
      statusDiv.textContent = `${extractedTables.length} tableau(x) extrait(s) avec succès!`;
    }, 2000);
  });
});

document.getElementById('closeBtn').addEventListener('click', () => {
  window.close();
});

