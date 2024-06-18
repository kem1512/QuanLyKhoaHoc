"use client";

import BillStatusHandler from "../../../../components/Handler/BillStatusHandler";

export default function DashboardBillStatusUpdate({
  params,
}: {
  params: { id: number };
}) {
  const { id } = params;

  return <BillStatusHandler id={id} />;
}
