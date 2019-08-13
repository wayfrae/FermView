var ctx = document.getElementById('profileChart').getContext('2d');
var myChart = new Chart(ctx, {
	type: 'line',
	data: data,
	options: options
});
