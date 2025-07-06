async function fetchStatus() {
  try {
    const res = await fetch(apiBase + '/api/status');
    const data = await res.json();

    ['supervisor', 'manager', 'worker', 'logger', 'monitor'].forEach(name => {
      const el = document.getElementById(name);
      const stateEl = el.querySelector('.state');
      const historyEl = el.querySelector('.history');

      if (data[name]) {
        stateEl.textContent = data[name].alive ? 'Alive' : 'Dead';
        stateEl.className = 'state ' + (data[name].alive ? 'alive' : 'dead');

        // History gösterimi
        if (data[name].history && data[name].history.length > 0) {
          historyEl.innerHTML = data[name].history
            .map(msg => `<div>${msg}</div>`)
            .join('');
        } else {
          historyEl.textContent = 'No history';
        }
      } else {
        stateEl.textContent = 'Unknown';
        stateEl.className = 'state dead';
        historyEl.textContent = '';
      }
    });
  } catch (e) {
    console.error('Error fetching status', e);
  }
}
