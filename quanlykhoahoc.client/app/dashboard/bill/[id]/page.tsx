"use client";

import BillHandler from "../../../../components/Handler/BillHandler";

export default function DashboardBillUpdate({
  params,
}: {
  params: { id: number };
}) {
  const { id } = params;

  return <BillHandler id={id} />;
}
