"use client";

import PracticeHandler from "../../../../components/Handler/PracticeHandler";

export default function DashboardPracticeUpdate({
  params,
}: {
  params: { id: number };
}) {
  const { id } = params;

  return <PracticeHandler id={id} />;
}
