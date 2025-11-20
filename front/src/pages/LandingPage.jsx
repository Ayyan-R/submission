import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import { FiArrowUpRight, FiCheck } from 'react-icons/fi'

export const LandingPage = () => {
  const [cards, setCards] = useState([])
  const [status, setStatus] = useState('loading')

  useEffect(() => {
    // Instead of API → Directly set your 3 bank cards
    const bankCards = [
      {
        id: 'hdfc',
        title: 'HDFC Bank',
        description: 'Monitor HDFC accounts, deposits, loans, cards and compliance.',
        badge: 'Premium',
        link: 'https://www.hdfcbank.com/'

      },
      {
        id: 'sbi',
        title: 'SBI',
        description: 'Realtime SBI sync with transactions, deposits and branch analytics.',
        badge: 'Popular',
        link: 'https://corp.onlinesbi.sbi/corporate/sbi/sbi_home.html'
      },
      {
        id: 'sc',
        title: 'Standard Chartered',
        description: 'Global visibility for SC accounts with cross-border insights.',
        badge: 'Global',
        link: 'https://www.sc.com/in/help/online-banking/'
      }
    ]

    setCards(bankCards)
    setStatus('success')
  }, [])

  return (
    <section className="space-y-8">
      <header className="grid gap-6 rounded-3xl bg-white p-8 shadow-lg lg:grid-cols-2">
      <div className="flex flex-col justify-center">
          <p className="text-m uppercase tracking-[0.5em] text-brand-500">Banking aggregator</p>
          <h1 className="text-4xl font-semibold text-slate-900">
            All of your bank accounts in one personal control center.
          </h1>
        </div>
        <div className="rounded-2xl bg-slate-900 p-6 text-white flex flex-col gap-4">
          <p className="text-m uppercase tracking-[0.5em] text-brand-200">Bank operations</p>
          <h2 className="text-2xl font-semibold">One hub for treasury</h2>
          <p className="text-sm text-slate-200">
            Provide executives and branch teams with a consistent view of balances and
            compliance attestations on mobile, tablet, or desktop.
          </p>
          <ul className="space-y-2 text-sm text-slate-200">
            {[
              ' Real-time syncing for all connected banks',
              'Treasury dashboard for balances & settlements',            
              'Unified page for transfers',
              'Full audit trails across every bank'
          
            ].map((item) => (
              <li key={item} className="flex items-center gap-2">
                <FiCheck className="text-brand-300" /> {item}
              </li>
            ))}
          </ul>
        </div>
      </header>

      <section className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
        {cards.map((card) => (
          <article
            key={card.id}
            className="rounded-2xl border border-slate-100 bg-white p-6 shadow-sm flex flex-col gap-3"
          >
            <span className="text-xs uppercase tracking-[0.4em] text-brand-500">
              {card.badge}
            </span>
            <h3 className="text-lg font-semibold text-slate-900">{card.title}</h3>
            <p className="text-sm text-slate-600 flex-1">{card.description}</p>
            <button className="inline-flex items-center gap-2 text-sm font-semibold text-brand-600">
            <a href={card.link} target="_blank" rel="noopener noreferrer">
  Explore
</a>
 <FiArrowUpRight />
            </button>
          </article>
        ))}
      </section>

      {status === 'loading' && (
        <p className="text-sm text-slate-500" role="status">Loading insight cards...</p>
      )}
      {status === 'error' && (
        <p className="text-sm text-rose-500" role="alert">
          API temporarily unavailable. Showing curated cards instead.
        </p>
      )}
    </section>
  )
}
