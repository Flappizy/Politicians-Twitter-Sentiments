import React, { useState, useEffect } from 'react';
import { Bar } from 'react-chartjs-2';
import { Chart as ChartJS } from 'chart.js/auto'

function BarChart({ numberOfPositiveTweets, numberOfNegativeTweets, numberOfNeutralTweets }) {

    const [data, setData] = useState({});

    useEffect(() => {
        setData(
            {
                labels: ['Positive', 'Negative', 'Neutral'],
                datasets: [
                    {
                        label: 'Sentiment',
                        backgroundColor: 'rgba(72, 61, 139)',
                        borderColor: 'rgba(75,192,192,1)',
                        borderWidth: 5,
                        borderRadius: 30,
                        data: [numberOfPositiveTweets, numberOfNegativeTweets, numberOfNeutralTweets]
                    }
                ]
            });
    }, [])

    return (
        <div>
            <Bar
                data={data}
                options={{
                    title: {
                        display: true,
                        text: '',
                        fontSize: 20
                    },
                    legend: {
                        display: true,
                        position: 'right'
                    }
                }}
            />
        </div>
    )
}

export default BarChart;
